using System.Linq.Expressions;

namespace FoodsharingWebAPI.Interfaces
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<bool> Add(T item);
        Task<bool> Update(T item);
        Task<bool> Delete(T item);
        Task<bool> Save();
        Task<IEnumerable<T>> GetWithInclude(params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetWithInclude(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties);
    }
}
