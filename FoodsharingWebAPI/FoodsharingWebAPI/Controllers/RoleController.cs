using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRepository<Role> roleRepository;
        private readonly ILogger<RoleController> logger;
        public RoleController(IRepository<Role> roleRepository, ILogger<RoleController> logger)
        {
            this.roleRepository = roleRepository;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<List<Role>>> GetAllAsync()
        {
            try
            {
                var roles = await roleRepository.GetAllAsync();
                if (roles != null)
                    return Ok(roles);
                else
                    return NotFound("Ролей не существует");
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при получении списка Role: {ex.Message}");
                return StatusCode(500, "Ошибка при получении списка ролей");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRoleAsync(int id)
        {
            try
            {
                var role = await roleRepository.GetByIdAsync(id);
                if (role != null)
                    return Ok(role);
                else
                    return NotFound("Роль с заданным id не найдена");
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при получении роли с id = {id}: {ex.Message}");
                return StatusCode(500, "Ошибка при получении роли");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoleAsync([FromBody] Role role)
        {
            if (role == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await roleRepository.AddAsync(role);
                return CreatedAtAction(nameof(GetRoleAsync), new { id = role.Id }, role);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при добавлении Role в БД: {ex.Message}");
                return StatusCode(500, "Ошибка при создании роли");
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRoleAsync([FromBody] Role roleUpdate)
        {
            if (roleUpdate == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await roleRepository.UpdateAsync(roleUpdate);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при обновлении Role: {ex.Message}");
                return StatusCode(500, "Ошибка при обновлении роли");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleAsync(int id)
        {
            try
            {
                var role = await roleRepository.GetByIdAsync(id);
                if (role == null)
                    return NotFound("Роли с таким id не существует");
                await roleRepository.DeleteAsync(role);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при удалении Role: {ex.Message}");
                return StatusCode(500, "Роль не была удалена");
            }
        }
    }
}
