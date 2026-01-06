using podilia_taxi_api.DbContext.Repository.IRepository;
using podilia_taxi_api.Models;

namespace podilia_taxi_api.DbContext.Repository
{
    public class RefreshTokenRepository: Repository<RefreshToken>, IRefreshTokenRepository
    {
        private readonly AppDbContext _context;
        public RefreshTokenRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task UpdateAsync(RefreshToken entity)
        {
            _context.RefreshTokens.Update(entity);
        }
    }
}
