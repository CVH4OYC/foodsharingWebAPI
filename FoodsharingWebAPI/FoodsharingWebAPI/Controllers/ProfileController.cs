using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : BaseController<Profile>
    {
        public ProfileController(IRepository<Profile> repository, ILogger<ProfileController> logger)
            : base(repository, logger) { }
    }
}
