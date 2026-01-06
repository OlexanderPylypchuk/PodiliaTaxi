namespace podilia_taxi_api.DbContext.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IRideRepository Rides { get; }
        IRideOrderRepository RideOrders { get; }

        IRefreshTokenRepository RefreshTokens { get; }

        Task SaveChangesAsync();
    }
}
