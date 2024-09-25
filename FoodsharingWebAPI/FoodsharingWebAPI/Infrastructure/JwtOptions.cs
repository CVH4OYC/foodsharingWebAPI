namespace FoodsharingWebAPI.Infrastructure
{
    /// <summary>
    /// Класс для описания свойств для генерации jwt-токена
    /// </summary>
    public class JwtOptions
    {
        public string SecretKey { get; set; } = string.Empty;
        public int ExpiresHours { get; set; }
    }
}
