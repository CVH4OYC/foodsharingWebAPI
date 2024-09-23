using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : BaseController<Message>
    {
        public MessageController(IRepository<Message> repository, ILogger<MessageController> logger)
            : base(repository, logger) { }
    }
}
