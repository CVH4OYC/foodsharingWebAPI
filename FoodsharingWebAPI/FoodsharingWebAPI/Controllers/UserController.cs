using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> userRepository;
        private readonly ILogger<UserController> logger;
        public UserController(IRepository<User> userRepository, ILogger<UserController> logger)
        {
            this.userRepository = userRepository;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await userRepository.GetAll();
            return Ok(users);
        }
        //[HttpGet("WithDetails")]
        //public async Task<IActionResult> GetAllWithDetails()
        //{
        //    var users = await userRepository.GetWithInclude(u => u.Profile, u => u.UserRoles);
        //    return Ok(users);
        //}

    }
}
