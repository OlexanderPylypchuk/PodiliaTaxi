using podilia_taxi_api.DbContext.Repository.IRepository;

namespace podilia_taxi_api.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        public TokenService(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }
    }
}
