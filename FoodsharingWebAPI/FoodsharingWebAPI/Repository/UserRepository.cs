using FoodsharingWebAPI.Data;
using FoodsharingWebAPI.Models;

namespace FoodsharingWebAPI.Repository
{
    // репозиторий для User
    public class UserRepository : Repository<User>
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}
