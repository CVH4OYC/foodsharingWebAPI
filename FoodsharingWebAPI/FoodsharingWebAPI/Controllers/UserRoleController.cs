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
        public async Task<ActionResult<List<UserRole>>> GetAll()
        {
            try
            {
                var UserRoles = await userRoleRepository.GetAll();
                if (UserRoles != null)
                    return Ok(UserRoles);
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
        public async Task<ActionResult<UserRole>> GetUserRole(int id)
        {
            try
            {
                var UserRole = await userRoleRepository.GetById(id);
                if (UserRole != null)
                    return Ok(UserRole);
                else
                    return NotFound("Запись о роли пользователя с заданным id не найдена");
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при получении записи \"Роль-пользователь\" с id = {id}: {ex.Message}");
                return StatusCode(500, "Ошибка при получении записи \"Роль-пользователь\"");
            }
        }
        [HttpGet("Detail")]
        public async Task<ActionResult<List<UserRole>>> GetAllDetail()
        {
            try
            {
                var UserRoles = await userRoleRepository.GetWithInclude(ur => ur.Role, ur => ur.User);
                if (UserRoles != null)
                    return Ok(UserRoles);
                else
                    return NotFound("Ещё ни один пользователь не имеет роли");
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при получении списка UserRole: {ex.Message}");
                return StatusCode(500, "Ошибка при получении списка ролей пользователей");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateUserRole([FromBody] UserRole UserRole)
        {
            if (UserRole == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await userRoleRepository.Add(UserRole);
                return CreatedAtAction(nameof(GetUserRole), new { id = UserRole.Id }, UserRole);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при добавлении UserRole в БД: {ex.Message}");
                return StatusCode(500, "Ошибка при создании записи \"Роль-пользователь\"");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserRole(int id)
        {
            try
            {
                var UserRole = await userRoleRepository.GetById(id);
                if (UserRole == null)
                    return NotFound("Записи \"Роль-пользователь\" с таким id не существует");
                await userRoleRepository.Delete(UserRole);
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
