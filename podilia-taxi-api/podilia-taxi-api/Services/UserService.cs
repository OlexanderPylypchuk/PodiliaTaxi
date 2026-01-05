using podilia_taxi_api.DbContext.Repository.IRepository;
using podilia_taxi_api.Models;
using podilia_taxi_api.Models.Dtos;

namespace podilia_taxi_api.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TokenDto> RefreshAccessToken(UserDto user, string refreshTokenHash)
        {
            // Logic to generate and return a new access token for the user
            return "new_access_token";
        }
    }
}
