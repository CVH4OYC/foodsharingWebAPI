using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IRepository<UserRole> userRoleRepository;
        private readonly ILogger<UserRoleController> logger;
        public UserRoleController(IRepository<UserRole> userRoleRepository, ILogger<UserRoleController> logger)
        {
            this.userRoleRepository = userRoleRepository;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<List<UserRole>>> GetAllAsync()
        {
            try
            {
                var userRoles = await userRoleRepository.GetAllAsync();
                if (userRoles != null)
                    return Ok(userRoles);
                else
                    return NotFound("Ещё ни один пользователь не имеет роли");
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при получении списка UserRole: {ex.Message}");
                return StatusCode(500, "Ошибка при получении списка ролей пользователей");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserRole>> GetUserRoleAsync(int id)
        {
            try
            {
                var userRole = await userRoleRepository.GetByIdAsync(id);
                if (userRole != null)
                    return Ok(userRole);
                else
                    return NotFound("Запись о роли пользователя с заданным id не найдена");
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при получении записи \"Роль-пользователь\" с id = {id}: {ex.Message}");
                return StatusCode(500, "Ошибка при получении записи \"Роль-пользователь\"");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateUserRoleAsync([FromBody] UserRole userRole)
        {
            if (userRole == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await userRoleRepository.AddAsync(userRole);
                return CreatedAtAction(nameof(GetUserRoleAsync), new { id = userRole.Id }, userRole);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при добавлении UserRole в БД: {ex.Message}");
                return StatusCode(500, "Ошибка при создании записи \"Роль-пользователь\"");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserRoleAsync(int id)
        {
            try
            {
                await userRoleRepository.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при удалении UserRole: {ex.Message}");
                return StatusCode(500, "Запись о роли пользователя не была удалена");
            }
        }
    }
}
