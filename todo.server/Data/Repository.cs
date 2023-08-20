using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace todo.server.Data.Interfaces
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public IEnumerable<T> Get(Expression<Func<T, bool>>? filter = null)
        {
            if(filter != null)
                return _dbSet.Where(filter).ToList();

            return _dbSet;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
            await SaveChangesAsync();
            return entities;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
