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
                var Roles = await roleRepository.GetAllAsync();
                if (Roles != null)
                    return Ok(Roles);
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
                var Role = await roleRepository.GetByIdAsync(id);
                if (Role != null)
                    return Ok(Role);
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
        public async Task<IActionResult> CreateRoleAsync([FromBody] Role Role)
        {
            if (Role == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await roleRepository.AddAsync(Role);
                return CreatedAtAction(nameof(GetRoleAsync), new { id = Role.Id }, Role);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при добавлении Role в БД: {ex.Message}");
                return StatusCode(500, "Ошибка при создании роли");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoleAsync(int id, [FromBody] Role RoleUpdate)
        {
            if (RoleUpdate == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                var Role = await roleRepository.GetByIdAsync(id);
                if (Role == null)
                    return NotFound("Роли с таким id не существует");

                if(!string.IsNullOrEmpty(RoleUpdate.Name))
                    Role.Name = RoleUpdate.Name;

                await roleRepository.UpdateAsync(Role);
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
                var Role = await roleRepository.GetByIdAsync(id);
                if (Role == null)
                    return NotFound("Роли с таким id не существует");
                await roleRepository.DeleteAsync(Role);
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
