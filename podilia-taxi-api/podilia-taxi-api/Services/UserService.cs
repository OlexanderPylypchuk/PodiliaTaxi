using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using podilia_taxi_api.DbContext.Repository.IRepository;
using podilia_taxi_api.Models;
using podilia_taxi_api.Models.Dtos;
using podilia_taxi_api.Utility;
using System.Security;

namespace podilia_taxi_api.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly TokenService _tokenService;
        private readonly HashService _hashService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly List<string> roles = new List<string> { SD.Role_Admin, SD.Role_Customer, SD.Role_Driver };
        public UserService(IUnitOfWork unitOfWork, TokenService tokenService, 
            HashService hashService, UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _hashService = hashService;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TokenDto> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users
                .Include(u => u.RefreshTokens)
                .SingleOrDefaultAsync(u =>
                    u.UserName == loginDto.UserNameOrEmail ||
                    u.Email == loginDto.UserNameOrEmail);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                throw new UnauthorizedAccessException("Invalid username or password.");

            if (!user.CanAuthenticate())
                throw new SecurityException("User is inactive.");

            // Revoke existing active refresh tokens (optional but recommended)
            foreach (var token in user.RefreshTokens.Where(t => t.IsActive))
            {
                token.RevokedAt = DateTime.UtcNow;
            }

            var accessToken = _tokenService.GenerateAccessToken(user);

            var refreshTokenValue = _tokenService.GenerateRefreshToken();
            var refreshTokenHash = _hashService.Hash(refreshTokenValue);

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshTokenHash,
                ExpiresAt = DateTime.UtcNow.AddDays(_configuration.GetValue<int>("JwtSettings:RefreshTokenExpirationInDays")),
                UserId = user.Id
            });

            await _unitOfWork.SaveChangesAsync();

            return new TokenDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenValue
            };
        }

        public async Task<User> Register(RegistrationDto registrationDto)
        {
            if (registrationDto == null)
            {
                throw new ArgumentNullException("Input is null");
            }

            var existingUser = await _unitOfWork.Users.GetSingle(u => u.Email == registrationDto.Email);

            if (existingUser != null)
            {
                throw new InvalidOperationException("User with the same username or email already exists.");
            }

            if(registrationDto.Password == null)
            {
                throw new ArgumentNullException("Password is null");
            }

            if(registrationDto.Phone == null)
            {
                throw new ArgumentNullException("Phone is null");
            }

            var user = new User
            {
                UserName = registrationDto.Email,
                Email = registrationDto.Email,
                PhoneNumber = registrationDto.Phone,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            IdentityResult result = await _userManager.CreateAsync(user, registrationDto.Password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(
                    string.Join("; ", result.Errors.Select(e => e.Description)));
            }

            await EnsureRolesExist();

            // Role assignment — server controlled
            var role = registrationDto.Role == SD.Role_Driver
                ? SD.Role_Driver
                : SD.Role_Customer;

            await _userManager.AddToRoleAsync(user, role);

            return user;
        }

        private async Task EnsureRolesExist()
        {
            

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public async Task<bool> AsignUserToRole(UserDto userDto)
        {
            if(userDto == null)
            {
                throw new ArgumentNullException("Input is null");
            }

            if(userDto.Role == null)
            {
                throw new ArgumentNullException("Role is null");
            }

            if(!roles.Contains(userDto.Role))
            {
                throw new InvalidOperationException("Role does not exist.");
            }

            var user = await _userManager.Users
                .SingleOrDefaultAsync(u =>
                    u.UserName == userDto.UserName ||
                    u.Email == userDto.Email);

            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            await EnsureRolesExist();

            await _userManager.RemoveFromRolesAsync(user, roles);

            await _userManager.AddToRoleAsync(user, userDto.Role);

            return true;
        }

        public async Task<TokenDto> RefreshAccessToken(string refreshTokenHash)
        {
            var user = await _unitOfWork.Users.GetSingle(
                u => u.RefreshTokens.Any(rt => rt.Token == refreshTokenHash),
                includeProperties: "RefreshTokens");

            if (user == null)
                throw new SecurityException("Invalid refresh token.");

            if (!user.CanAuthenticate())
                throw new SecurityException("User is inactive.");

            var refreshToken = user.RefreshTokens
                .SingleOrDefault(rt => rt.Token == refreshTokenHash);

            if (refreshToken == null)
                throw new SecurityException("Invalid refresh token.");

            if (refreshToken.RevokedAt != null)
            {
                foreach (var token in user.RefreshTokens.Where(t => t.IsActive))
                {
                    token.RevokedAt = DateTime.UtcNow;
                }

                await _unitOfWork.SaveChangesAsync();
                throw new SecurityException("Refresh token reuse detected.");
            }

            if (refreshToken.IsExpired)
                throw new SecurityException("Refresh token expired.");

            var newAccessToken = _tokenService.GenerateAccessToken(user);

            var newRefreshTokenValue = _tokenService.GenerateRefreshToken();
            var newRefreshTokenHash = _hashService.Hash(newRefreshTokenValue);

            refreshToken.RevokedAt = DateTime.UtcNow;
            refreshToken.ReplacedByToken = newRefreshTokenHash;

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshTokenHash,
                ExpiresAt = DateTime.UtcNow.AddDays(
                    _configuration.GetValue<int>("JwtSettings:RefreshTokenExpirationInDays")),
                UserId = user.Id
            });

            await _unitOfWork.SaveChangesAsync();

            return new TokenDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshTokenValue
            };
        }

        public async Task RateUser(RatingDto ratingDto)
        {
            if (ratingDto == null)
            {
                throw new ArgumentNullException("Input is null");
            }

            if (ratingDto.Score < 1 || ratingDto.Score > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(ratingDto.Score),
                    "Score must be between 1 and 5.");
            }
            var raterId = _httpContextAccessor.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(raterId))
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            if (raterId == ratingDto.RatedId)
            {
                throw new InvalidOperationException("User cannot rate themselves.");
            }

            var existingRating = await _unitOfWork.Ratings.GetSingle(r =>
                r.RaterId == raterId &&
                r.RatedId == ratingDto.RatedId);

            if (existingRating != null)
            {
                existingRating.Score = ratingDto.Score;
                await _unitOfWork.SaveChangesAsync();
                return;
            }

            var rater = await _unitOfWork.Users.GetSingle(u => u.Id == raterId);
            var rated = await _unitOfWork.Users.GetSingle(u => u.Id == ratingDto.RatedId);

            if (rater == null || rated == null)
            {
                throw new InvalidOperationException("Rater or Ratee not found.");
            }

            var raterRole = (await _userManager.GetRolesAsync(rater)).FirstOrDefault();
            var ratedRole = (await _userManager.GetRolesAsync(rated)).FirstOrDefault();

            if (raterRole == ratedRole)
            {
                throw new InvalidOperationException("Rater and Ratee cannot have the same role.");
            }

            var rating = new Rating
            {
                RaterId = raterId,
                RatedId = ratingDto.RatedId,
                Score = ratingDto.Score,
            };
            await _unitOfWork.Ratings.Add(rating);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
