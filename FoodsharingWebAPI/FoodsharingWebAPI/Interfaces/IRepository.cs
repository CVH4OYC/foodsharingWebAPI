using System.Linq.Expressions;

namespace FoodsharingWebAPI.Interfaces
{
    /// <summary>
    /// Интерфейс универсального репозитория
    /// </summary>
    public interface IRepository<T> : IDisposable
        where T : class
    {
        /// <summary>
        /// Метод репозитория для получения всех сущностей из БД
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken=default);
        /// <summary>
        /// Метод репозитория для получения сущности из БД по id
        /// </summary>
        Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        /// <summary>
        /// Метод репозитория для добавления сущности в БД
        /// </summary>
        Task AddAsync(T item, CancellationToken cancellationToken = default);
        /// <summary>
        /// Метод репозитория для обновления сущности в БД
        /// </summary>
        Task UpdateAsync(T item, CancellationToken cancellationToken = default);
        /// <summary>
        /// Метод репозитория для удаления сущности из БД
        /// </summary>
        Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default);
        /// <summary>
        /// Метод репозитория для получения сущностей с включением связанных данных
        /// </summary>
        Task<IEnumerable<T>> GetWithIncludeAsync(CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties);
        /// <summary>
        /// Метод репозитория для получения сущностей с включением связанных данных по какому-то условию
        /// </summary>
        Task<IEnumerable<T>> GetWithIncludeAsync(Func<T, bool> predicate, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties);
    }
}
