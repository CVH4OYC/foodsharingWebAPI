﻿using FoodsharingWebAPI.DTO;
using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using FoodsharingWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodsharingWebAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с пользователями
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController<User>
    {
        private readonly IUserRepository repository;

        /// <summary>
        /// Конструктор контроллера пользователей
        /// </summary>
        /// <param name="repository">Репозиторий пользователей</param>
        /// <param name="logger">Логгер для записи информации</param>
        public UserController(IUserRepository repository, ILogger<UserController> logger)
            : base(repository, logger) 
        {
            this.repository = repository;
        }
        /// <summary>
        /// Метод, обрабатывающий маршрут для регистрации пользователя через <see cref="UserService"/>
        /// </summary>
        /// <param name="userService">Сервис для управления регистрацией и входом пользователей</param>
        /// <param name="request">Запрос, содержащий имя пользователя и пароль</param>
        /// <returns>
        /// Код состояния:<br/>
        /// - 200 OK: если регистрация прошла успешно<br/>
        /// - 400 Bad Request: если регистрация завершилась неудачно
        /// </returns>
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
        /// Метод, обрабатывающий маршрут для входа пользователя по имени пользователя и паролю (добавляет сгенерированный jwt токен в куки)
        /// </summary>
        /// <param name="userService">Сервис для управления регистрацией и входом пользователей</param>
        /// <param name="request">Запрос, содержащий имя пользователя и пароль</param>
        /// <returns>
        /// Код состояния:<br/>
        /// - 200 OK: если вход выполнен успешно<br/>
        /// - 400 Bad Request: если вход завершился неудачно
        /// </returns>
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