using FoodsharingWebAPI.Models;

namespace FoodsharingWebAPI.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Метод для получения User по UserName
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="cancellationToken">Токен отмены операции (по умолчанию None)</param>
        /// <returns>Пользователь типа <see cref="User"/></returns>
        Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken);
    }
}
