using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : BaseController<Transaction>
    {
        public TransactionController(IRepository<Transaction> repository, ILogger<TransactionController> logger)
            : base(repository, logger) { }
    }
}
