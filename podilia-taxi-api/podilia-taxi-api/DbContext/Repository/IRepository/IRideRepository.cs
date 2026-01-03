using podilia_taxi_api.Models;

namespace podilia_taxi_api.DbContext.Repository.IRepository
{
    public interface IRideRepository : IRepository<Ride>
    {
        Task<Ride> UpdateAsync(Ride entity);
    }
}
