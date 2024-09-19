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
        public async Task<ActionResult<List<User>>> GetAll()
        {
            var users = await userRepository.GetAll();
            if (users != null)
                return Ok(users);
            else
                return NotFound();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await userRepository.GetById(id);
            if (user != null)
                return Ok(user);
            else
                return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            bool result = await userRepository.Add(user);
            if (result)
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            else
                return BadRequest();
        }

    }
}
