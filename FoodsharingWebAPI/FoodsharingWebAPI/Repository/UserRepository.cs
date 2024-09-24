using FoodsharingWebAPI.Data;
using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodsharingWebAPI.Repository
{
    public class UserRepository: Repository<User>, IUserRepository
    {
        private readonly DbContext context;

        public UserRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken)
        {
            return await context.Set<User>().FirstOrDefaultAsync(u => u.UserName == userName, cancellationToken);
        }
    }
}
