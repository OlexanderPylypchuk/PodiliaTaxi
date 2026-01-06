using podilia_taxi_api.Models;

namespace podilia_taxi_api.DbContext.Repository.IRepository
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        Task UpdateAsync(RefreshToken entity);
    }
}
