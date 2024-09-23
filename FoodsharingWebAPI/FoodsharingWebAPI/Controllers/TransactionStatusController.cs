using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionStatusController : BaseController<TransactionStatus>
    {
        public TransactionStatusController(IRepository<TransactionStatus> repository, ILogger<TransactionStatusController> logger)
            : base(repository, logger) { }
    }
}
