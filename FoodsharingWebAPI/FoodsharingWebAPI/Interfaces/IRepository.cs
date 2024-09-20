using System.Linq.Expressions;

namespace FoodsharingWebAPI.Interfaces
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(T item);
        Task<IEnumerable<T>> GetWithIncludeAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetWithIncludeAsync(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties);
    }
}
