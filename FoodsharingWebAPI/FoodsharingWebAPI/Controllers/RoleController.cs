using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : BaseController<Role>
    {
        public RoleController(IRepository<Role> repository, ILogger<RoleController> logger)
            : base(repository, logger) { }
    }
}
