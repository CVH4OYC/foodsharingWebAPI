using FoodsharingWebAPI.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FoodsharingWebAPI.Extensions
{
    /// <summary>
    /// Класс расширений
    /// </summary>
    public static class ApiExtensions
    {
        /// <summary>
        /// Метод, добавляющий jwt-аутентификацию и авторизацию в коллекцию сервисов
        /// и настраивающий параметры валидации токенов
        /// </summary>
        /// <param name="services">Коллекция сервисов для DI-контейнера</param>
        /// <param name="configuration">Конфигурация приложения, содержащая параметры для JWT</param>
        public static void AddApiAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // укзывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,
                        // будет ли валидироваться потребитель токена
                        ValidateAudience = true,
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,

                        // установка ключа безопасности
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true,
                    };
                    // чтение установленного токена из кук
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["tochno_ne_jwt_token"];

                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddAuthorization();
        }
    }
}
