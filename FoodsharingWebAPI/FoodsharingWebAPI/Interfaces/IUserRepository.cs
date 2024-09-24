using FoodsharingWebAPI.Models;

namespace FoodsharingWebAPI.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Метод для получения User по UserName
        /// </summary>
        Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken);
    }
}
