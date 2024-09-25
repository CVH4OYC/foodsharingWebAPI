namespace FoodsharingWebAPI.Interfaces
{
    public interface IPasswordHasher
    {
        /// <summary>
        /// Метод, генерирующий хеш пароля
        /// </summary>
        string Generate(string password);
        /// <summary>
        /// Метод, сравнивающий введённый пароль и хеш
        /// </summary>
        bool Verify(string password, string passwordHash);
    }
}