using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : BaseController<Category>
    {
        public CategoryController(IRepository<Category> repository, ILogger<CategoryController> logger)
            : base(repository, logger) { }
    }
}
