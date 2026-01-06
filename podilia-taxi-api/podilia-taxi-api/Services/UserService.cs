using Microsoft.EntityFrameworkCore;
using podilia_taxi_api.DbContext.Repository.IRepository;
using podilia_taxi_api.Models;
using podilia_taxi_api.Models.Dtos;
using System.Security;

namespace podilia_taxi_api.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;
        private readonly HashService _hashService;
        public UserService(IUnitOfWork unitOfWork, TokenService tokenService, HashService hashService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _hashService = hashService;
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
                .Single(rt => rt.Token == refreshTokenHash);

            // Reuse attack detection
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

            // Normal rotation
            var newAccessToken = _tokenService.GenerateAccessToken(user);

            var newRefreshTokenValue = _tokenService.GenerateRefreshToken();
            var newRefreshTokenHash = _hashService.Hash(newRefreshTokenValue);

            refreshToken.RevokedAt = DateTime.UtcNow;
            refreshToken.ReplacedByToken = newRefreshTokenHash;

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshTokenHash,
                ExpiresAt = DateTime.UtcNow.AddDays(30),
                UserId = user.Id
            });

            await _unitOfWork.SaveChangesAsync();

            return new TokenDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshTokenValue
            };
        }
    }
}
