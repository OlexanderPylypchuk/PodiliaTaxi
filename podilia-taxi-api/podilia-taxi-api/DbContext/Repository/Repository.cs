using Microsoft.EntityFrameworkCore;
using podilia_taxi_api.DbContext.Repository.IRepository;
using podilia_taxi_api.Models;
using System.Linq.Expressions;

namespace podilia_taxi_api.DbContext.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null, int? pageSize = 10, int? pageNumber = 0, string? includeProperties = null, bool? allowDeleted = false)
        {
            var query = _dbSet.AsQueryable();
            
            if(filter != null)
            {
                query = query.Where(filter);
            }

            if(pageSize != null)
            {
                query = query.Skip(pageSize.Value * pageNumber.Value).Take(pageSize.Value);
            }

            if(includeProperties != null)
            {
                foreach(var includeProperty in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if(allowDeleted == false && typeof(IBaseEntity).IsAssignableFrom(typeof(T)))
            {
                query = query.Where(e => EF.Property<DateTime?>(e, nameof(IBaseEntity.DeletedAt)) != null);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetSingle(Expression<Func<T, bool>> filter, string? includeProperties = null, bool? allowDeleted = false)
        {
            var query = _dbSet.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (allowDeleted == false && typeof(IBaseEntity).IsAssignableFrom(typeof(T)))
            {
                query = query.Where(e => EF.Property<DateTime?>(e, nameof(IBaseEntity.DeletedAt)) != null);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
