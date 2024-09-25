using FoodsharingWebAPI.DTO;
using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using FoodsharingWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodsharingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController<User>
    {
        private readonly IUserRepository repository;
        public UserController(IUserRepository repository, ILogger<UserController> logger)
            : base(repository, logger) 
        {
            this.repository = repository;
        }
        [HttpPost("reg")]
        public async Task<IActionResult> Register(UserService userService, RegLogUserRequest request)
        {
            await userService.Register(request.UserName, request.Password);
            return Ok();
        }
        [HttpPost("log")]
        public async Task<IActionResult> Login(UserService userService, RegLogUserRequest reguest)
        {
            var token = await userService.Login(reguest.UserName, reguest.Password);
            Response.Cookies.Append("tochno_ne_jwt_token", token);
            return Ok();
        }

    }
}