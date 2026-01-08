using podilia_taxi_api.DbContext.Repository.IRepository;

namespace podilia_taxi_api.DbContext.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        public IUserRepository Users { get; }

        public IRideRepository Rides { get; }
        public IRatingRepository Ratings { get; }
        public IRideOrderRepository RideOrders { get; }
        public IRefreshTokenRepository RefreshTokens { get; }

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            Users = new UserRepository(db);
            Rides = new RideRepository(db);
            RideOrders = new RideOrderRepository(db);
            RefreshTokens = new RefreshTokenRepository(db);
            Ratings = new RatingRepository(db);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
