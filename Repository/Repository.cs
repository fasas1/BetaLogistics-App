using Microsoft.EntityFrameworkCore;
using BetaLogistics.Data;
using BetaLogistics.Repository.IRepository;
using System.Linq.Expressions;

namespace BetaLogistics.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProperties = null, int pageSize = 3, int pageNumber = 1)
        {
            IQueryable<T> query = dbSet;
            if(filter != null)
            {
                query = query.Where(filter);
            }
            if(pageSize > 0)
            {
              if(pageNumber > 100)
                {
                    pageSize = 100;
                }
              query = query.Skip(pageSize*(pageNumber-1)).Take(pageSize);
            }
            // Include the specified navigation properties
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            // Include the specified navigation properties
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

    

    public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);
            await SaveAsync();
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
