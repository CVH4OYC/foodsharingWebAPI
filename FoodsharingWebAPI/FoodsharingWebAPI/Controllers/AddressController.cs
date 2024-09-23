using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : BaseController<Address>
    {
        public AddressController(IRepository<Address> repository, ILogger<AddressController> logger)
            : base(repository, logger) { }
    }
}
