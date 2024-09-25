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
        public async Task Register(string userName, string password, CancellationToken cancellationToken=default)
        {
            var hashedPassword = passwordHasher.Generate(password);
            await userRepository.AddAsync(new Models.User { UserName = userName, Password = hashedPassword }, cancellationToken);
        } 
        public async Task<string> Login (string userName, string password, CancellationToken cancellationToken=default)
        {
            var user = await userRepository.GetByUserNameAsync(userName, cancellationToken);
            if (user == null)
            {
                throw new Exception("Пользователь с таким именем не найден");
            }
            var result = passwordHasher.Verify(password, user.Password);
            if (!result)
            {
                throw new Exception("Ошибка входа, неверный пароль");
            }

            var token = jwtProvider.GenerateToken(user);
            return token;
        }

    }
}
