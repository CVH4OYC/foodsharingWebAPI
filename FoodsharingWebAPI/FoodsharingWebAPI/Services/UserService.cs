using FoodsharingWebAPI.Infrastructure;
using FoodsharingWebAPI.Interfaces;

namespace FoodsharingWebAPI.Services
{
    public class UserService
    {
        private readonly IPasswordHasher passwordHasher;
        private readonly IUserRepository userRepository;
        private readonly IJwtProvider jwtProvider;

        public UserService(IPasswordHasher passwordHasher, IUserRepository userRepository, IJwtProvider jwtProvider)
        {
            this.passwordHasher = passwordHasher;
            this.userRepository = userRepository;
            this.jwtProvider = jwtProvider;
        }
        /// <summary>
        /// Метод регистрации нового пользователя
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="password">Пароль</param>
        /// <param name="cancellationToken">Токен отмены операции (по умолчанию None)</param>
        /// <returns>Результат операции типа <see cref="OperationResult"/></returns>
        public async Task<OperationResult> RegisterAsync(string userName, string password, CancellationToken cancellationToken=default)
        {
            var user = await userRepository.GetByUserNameAsync(userName, cancellationToken);
            if (user != null)
                return OperationResult.FailureResult("Пользователь с таким именем уже существует");

            var hashedPassword = passwordHasher.Generate(password);
            await userRepository.AddAsync(new Models.User { UserName = userName, Password = hashedPassword }, cancellationToken);

            return OperationResult.SuccessResult("Регистрация прошла успешно");
        }
        /// <summary>
        /// Метод идентификации и аутентификации пользователя по имени пользователя
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="password">Пароль</param>
        /// <param name="cancellationToken">Токен отмены операции (по умолчанию None)</param>
        /// <returns>Результат операции типа <see cref="OperationResult"/></returns>
        public async Task<OperationResult> LoginAsync (string userName, string password, CancellationToken cancellationToken=default)
        {
            var user = await userRepository.GetByUserNameAsync(userName, cancellationToken);
            if (user == null)
                return OperationResult.FailureResult("Пользователь с таким именем не найден");
            var result = passwordHasher.Verify(password, user.Password);
            if (!result)
                return OperationResult.FailureResult("Ошибка входа, неверный пароль");

            var token = jwtProvider.GenerateToken(user);
            return OperationResult.SuccessResult("Вход выполнен успешно", token);
        }
    }
}
