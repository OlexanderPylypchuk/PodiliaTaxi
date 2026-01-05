using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using podilia_taxi_api.DbContext.Repository.IRepository;

namespace podilia_taxi_api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
