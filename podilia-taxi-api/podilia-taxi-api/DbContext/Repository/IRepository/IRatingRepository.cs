using podilia_taxi_api.Models;

namespace podilia_taxi_api.DbContext.Repository.IRepository
{
    public interface IRatingRepository : IRepository<Rating>
    {
        Task UpdateAsync(Rating entity);
    }
}
