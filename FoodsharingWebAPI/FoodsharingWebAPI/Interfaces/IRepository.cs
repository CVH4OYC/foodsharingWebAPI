using System.Linq.Expressions;

namespace FoodsharingWebAPI.Interfaces
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken=default);
        Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(T item, CancellationToken cancellationToken = default);
        Task UpdateAsync(T item, CancellationToken cancellationToken = default);
        Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetWithIncludeAsync(CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetWithIncludeAsync(Func<T, bool> predicate, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties);
    }
}
