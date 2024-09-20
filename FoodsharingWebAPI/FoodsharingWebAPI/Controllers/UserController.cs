using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                var users = await userRepository.GetAll();
                if (users != null)
                    return Ok(users);
                else
                    return NotFound("Пользователей не существует");
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при получении списка User: {ex.Message}");
                return StatusCode(500,"Ошибка при получении списка пользователей");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                var user = await userRepository.GetById(id);
                if (user != null)
                    return Ok(user);
                else
                    return NotFound("Пользователь с заданным id не найден");
            }
            catch(Exception ex) 
            {
                logger.LogError($"Ошибка при получении пользователя с id = {id}: {ex.Message}");
                return StatusCode(500,"Ошибка при получении пользователя по id");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await userRepository.Add(user);
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при добавлении User в БД: {ex.Message}");
                return StatusCode(500,"Ошибка при создании пользователя");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User userUpdate)
        {
            if (userUpdate == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                var user = await userRepository.GetById(id);
                if (user == null)
                    return NotFound("Пользователя с таким id не существует");
                if (!string.IsNullOrEmpty(userUpdate.UserName))
                    user.UserName = userUpdate.UserName;
                if (!string.IsNullOrEmpty(userUpdate.Password)) // это временно, потом нужно хеш записывать
                    user.Password = userUpdate.Password;

                await userRepository.Update(user);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при обновлении User: {ex.Message}");
                return StatusCode(500, "Ошибка при обновлении пользователя");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await userRepository.GetById(id);
                if (user == null)
                    return NotFound("Пользователя с таким id не существует");
                await userRepository.Delete(user);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при удалении User: {ex.Message}");
                return StatusCode(500, "Пользователь не был удалён");
            }
        }
    }
}
