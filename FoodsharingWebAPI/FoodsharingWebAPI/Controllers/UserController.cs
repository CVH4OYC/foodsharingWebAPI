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
        public async Task<ActionResult<List<User>>> GetAllAsync()
        {
            try
            {
                var users = await userRepository.GetAllAsync();
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
        public async Task<ActionResult<User>> GetUserAsync(int id)
        {
            try
            {
                var user = await userRepository.GetByIdAsync(id);
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
        public async Task<IActionResult> CreateUserAsync([FromBody] User user)
        {
            if (user == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await userRepository.AddAsync(user);
                return CreatedAtAction(nameof(GetUserAsync), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при добавлении User в БД: {ex.Message}");
                return StatusCode(500,"Ошибка при создании пользователя");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] User userUpdate)
        {
            if (userUpdate == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                var user = await userRepository.GetByIdAsync(id);
                if (user == null)
                    return NotFound("Пользователя с таким id не существует");
                if (!string.IsNullOrEmpty(userUpdate.UserName))
                    user.UserName = userUpdate.UserName;
                if (!string.IsNullOrEmpty(userUpdate.Password)) // это временно, потом нужно хеш записывать
                    user.Password = userUpdate.Password;

                await userRepository.UpdateAsync(user);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при обновлении User: {ex.Message}");
                return StatusCode(500, "Ошибка при обновлении пользователя");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            try
            {
                var user = await userRepository.GetByIdAsync(id);
                if (user == null)
                    return NotFound("Пользователя с таким id не существует");
                await userRepository.DeleteAsync(user);
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
