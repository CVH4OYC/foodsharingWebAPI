using FoodsharingWebAPI.Models;

namespace FoodsharingWebAPI.Interfaces
{
    public interface IJwtProvider
    {
        /// <summary>
        /// Метод генерации jwt-токена
        /// </summary>
        string GenerateToken(User user);
    }
}