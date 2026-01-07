using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using podilia_taxi_api.DbContext.Repository.IRepository;
using podilia_taxi_api.Services;

namespace podilia_taxi_api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserService _userService;
        public AuthController(IUnitOfWork unitOfWork, UserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Login()
        {
            // Authentication logic goes here
            return Ok("Authenticated");
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register()
        {
            // Registration logic goes here
            return Ok("Registered");
        }
    }
}
