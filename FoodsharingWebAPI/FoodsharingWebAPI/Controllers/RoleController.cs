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
        private readonly IRepository<Role> RoleRepository;
        private readonly ILogger<RoleController> logger;
        public RoleController(IRepository<Role> RoleRepository, ILogger<RoleController> logger)
        {
            this.RoleRepository = RoleRepository;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<List<Role>>> GetAll()
        {
            try
            {
                var Roles = await RoleRepository.GetAll();
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
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            try
            {
                var Role = await RoleRepository.GetById(id);
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
        public async Task<IActionResult> CreateRole([FromBody] Role Role)
        {
            if (Role == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await RoleRepository.Add(Role);
                return CreatedAtAction(nameof(GetRole), new { id = Role.Id }, Role);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при добавлении Role в БД: {ex.Message}");
                return StatusCode(500, "Ошибка при создании роли");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] Role RoleUpdate)
        {
            if (RoleUpdate == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                var Role = await RoleRepository.GetById(id);
                if (Role == null)
                    return NotFound("Роли с таким id не существует");

                if(!string.IsNullOrEmpty(RoleUpdate.Name))
                    Role.Name = RoleUpdate.Name;

                await RoleRepository.Update(Role);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при обновлении Role: {ex.Message}");
                return StatusCode(500, "Ошибка при обновлении роли");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            try
            {
                var Role = await RoleRepository.GetById(id);
                if (Role == null)
                    return NotFound("Роли с таким id не существует");
                await RoleRepository.Delete(Role);
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
