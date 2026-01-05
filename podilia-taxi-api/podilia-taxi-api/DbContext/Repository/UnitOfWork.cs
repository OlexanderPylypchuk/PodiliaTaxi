using podilia_taxi_api.DbContext.Repository.IRepository;

namespace podilia_taxi_api.DbContext.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository Users { get; }

        public IRideRepository Rides { get; }

        public IRideOrderRepository RideOrders { get; }

        public UnitOfWork(AppDbContext db)
        {
            Users = new UserRepository(db);
            Rides = new RideRepository(db);
            RideOrders = new RideOrderRepository(db);
        }
    }
}
