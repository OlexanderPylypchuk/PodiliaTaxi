using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using podilia_taxi_api.DbContext.Repository.IRepository;
using podilia_taxi_api.Models;
using podilia_taxi_api.Models.Dtos;
using podilia_taxi_api.Utility;
using System.Security.Claims;

namespace podilia_taxi_api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (currentUserId != id &&
                        !User.IsInRole(SD.Role_Admin) &&
                        !User.IsInRole(SD.Role_Moderator))
                {
                    return Forbid();
                }
                var user = await _unitOfWork.Users.GetSingle(u => u.Id == id);
                if (user != null)
                {
                    var result = _mapper.Map<UserDto>(user);
                    return Ok(result);
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("all")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _unitOfWork.Users.GetAll();
                var result = _mapper.Map<IEnumerable<UserDto>>(users);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("add-rating")]
        public async Task<IActionResult> AddRating(RatingDto ratingDto)
        {
            try
            {
                var rating = _mapper.Map<Rating>(ratingDto);

                return Ok("Rating added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
