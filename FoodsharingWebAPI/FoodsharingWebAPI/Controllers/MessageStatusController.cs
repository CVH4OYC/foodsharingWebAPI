using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageStatusController : BaseController<MessageStatus>
    {
        public MessageStatusController(IRepository<MessageStatus> repository, ILogger<MessageStatusController> logger)
            : base(repository, logger) { }
    }
}
