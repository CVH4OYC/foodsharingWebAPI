using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationController : BaseController<Organization>
    {
        public OrganizationController(IRepository<Organization> repository, ILogger<OrganizationController> logger)
            : base(repository, logger) { }
    }
}
