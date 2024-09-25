using FoodsharingWebAPI.Models;

namespace FoodsharingWebAPI.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}