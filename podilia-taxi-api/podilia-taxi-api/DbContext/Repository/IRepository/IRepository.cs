using System.Linq.Expressions;

namespace podilia_taxi_api.DbContext.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        public Task<T> GetSingle(Expression<Func<T, bool>> filter, string? includeProperties = null, bool? allowDeleted = false);
        public Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null, int? pageSize = 10, int? pageNumber = 0, string? includeProperties = null, bool? allowDeleted = false);
        public Task Add(T entity);
        public Task Remove(T entity);
    }
}
