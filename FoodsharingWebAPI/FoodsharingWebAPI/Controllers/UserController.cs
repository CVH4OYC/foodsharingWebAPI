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
        /// <summary>
        /// Метод, обрабатывающий маршрут для регистрации пользователя через <see cref="UserService"/>
        /// </summary>
        [HttpPost("reg")]
        public async Task<IActionResult> RegisterAsync(UserService userService, RegLogUserRequest request)
        {
            var result = await userService.RegisterAsync(request.UserName, request.Password);
            if (result.Success)
                return Ok(result.Message);
            else
                return BadRequest(result.Message);
        }
        /// <summary>
        /// Метод, обрабатывающий маршрут для входа пользователя по имени пользователя и паролю
        /// </summary>
        [HttpPost("log")]
        public async Task<IActionResult> LoginAsync(UserService userService, RegLogUserRequest reguest)
        {
            var result = await userService.LoginAsync(reguest.UserName, reguest.Password);
            if (result.Success)
            {
                Response.Cookies.Append("tochno_ne_jwt_token", result.Data);
                return Ok(result.Message);
            }
            else
                return BadRequest(result.Message);
        }
    }
}