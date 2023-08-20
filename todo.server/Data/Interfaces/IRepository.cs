using System.Linq.Expressions;

namespace todo.server.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public IEnumerable<T> Get(Expression<Func<T, bool>>? filter = null);
        public Task<T> CreateAsync(T entity);
        public Task<T> UpdateAsync(T entity);
        public Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities);
        public Task DeleteAsync(T entity);
        public Task DeleteRangeAsync(IEnumerable<T> entities);
    }
}
