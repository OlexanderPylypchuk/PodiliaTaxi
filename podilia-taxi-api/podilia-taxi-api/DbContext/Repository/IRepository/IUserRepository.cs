using podilia_taxi_api.Models;

namespace podilia_taxi_api.DbContext.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        Task UpdateAsync(User entity);
    }
}
