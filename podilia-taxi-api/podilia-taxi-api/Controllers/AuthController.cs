using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using podilia_taxi_api.DbContext.Repository.IRepository;
using podilia_taxi_api.Models.Dtos;
using podilia_taxi_api.Services;
using podilia_taxi_api.Utility;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace podilia_taxi_api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                var result = await _userService.Login(loginDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegistrationDto registrationDto)
        {
            try
            {
                var result = await _userService.Register(registrationDto);
                if (result != null)
                {
                    return Ok("User registered successfully");
                }
                else
                {
                    return BadRequest("User registration failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            try
            {
                var result = await _userService.RefreshAccessToken(refreshTokenDto.RefreshToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Admin)]
        [Route("change-role")]
        public async Task<IActionResult> ChangeUserRole(UserDto userDto)
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (currentUserId == userDto.Id)
                {
                    return BadRequest("You cannot change your own role");
                }

                var result = await _userService.AsignUserToRole(userDto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
