using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRoleController : BaseController<UserRole>
    {
        public UserRoleController(IRepository<UserRole> repository, ILogger<UserRoleController> logger)
            : base(repository, logger) { }
    }
}
