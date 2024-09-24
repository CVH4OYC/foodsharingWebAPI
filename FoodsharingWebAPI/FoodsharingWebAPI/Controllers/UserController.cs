﻿using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodsharingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController<User>
    {
        public UserController(IUserRepository repository, ILogger<UserController> logger)
            : base(repository, logger) { }
        public async Task<IActionResult> Register()
        {
            return Ok();
        }
        public async Task<IActionResult> Login()
        {
            return Ok();
        }

    }
}