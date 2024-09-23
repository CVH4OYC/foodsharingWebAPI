using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : BaseController<Chat>
    {
        public ChatController(IRepository<Chat> repository, ILogger<ChatController> logger)
            : base(repository, logger) { }
    }
}
