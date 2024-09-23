using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RepresentativeController : BaseController<Representative>
    {
        public RepresentativeController(IRepository<Representative> repository, ILogger<RepresentativeController> logger)
            : base(repository, logger) { }
    }
}
