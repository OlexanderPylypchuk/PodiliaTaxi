using podilia_taxi_api.DbContext.Repository.IRepository;
using podilia_taxi_api.Models;

namespace podilia_taxi_api.DbContext.Repository
{
    public class RideRepository : Repository<Ride>, IRideRepository
    {
        private readonly AppDbContext _db;

        public RideRepository(AppDbContext context) : base(context)
        {
            _db = context;
        }

        public async Task UpdateAsync(Ride entity)
        {
            _db.Rides.Update(entity);
        }
    }
}
