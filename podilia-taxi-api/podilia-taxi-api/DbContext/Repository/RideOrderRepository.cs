using podilia_taxi_api.DbContext.Repository.IRepository;
using podilia_taxi_api.Models;

namespace podilia_taxi_api.DbContext.Repository
{
    public class RideOrderRepository : Repository<RideOrder>, IRideOrderRepository
    {
        private readonly AppDbContext _db;

        public RideOrderRepository(AppDbContext context) : base(context)
        {
            _db = context;
        }
        public async Task UpdateAsync(RideOrder entity)
        {
            _db.RideOrders.Update(entity);
        }
    }
}
