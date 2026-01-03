using podilia_taxi_api.DbContext.Repository.IRepository;
using podilia_taxi_api.Models;

namespace podilia_taxi_api.DbContext.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext context) : base(context)
        {
            _db = context;
        }

        public async Task<User> UpdateAsync(User entity)
        {
            _db.Users.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
