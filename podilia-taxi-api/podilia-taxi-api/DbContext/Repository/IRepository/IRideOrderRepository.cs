using podilia_taxi_api.Models;

namespace podilia_taxi_api.DbContext.Repository.IRepository
{
    public interface IRideOrderRepository : IRepository<RideOrder>
    {
        Task UpdateAsync(RideOrder entity);
    }
}
