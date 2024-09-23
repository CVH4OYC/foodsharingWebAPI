using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IRepository<Organization> organizationRepository;
        private readonly ILogger<OrganizationController> logger;
        public OrganizationController(IRepository<Organization> organizationRepository, ILogger<OrganizationController> logger)
        {
            this.organizationRepository = organizationRepository;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<List<Organization>>> GetAllAsync()
        {
            try
            {
                var organizations = await organizationRepository.GetAllAsync();
                if (organizations != null)
                    return Ok(organizations);
                else
                    return NotFound("Организаций не существует");
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при получении списка Organization: {ex.Message}");
                return StatusCode(500, "Ошибка при получении списка организаций");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Organization>> GetOrganizationAsync(int id)
        {
            try
            {
                var organization = await organizationRepository.GetByIdAsync(id);
                if (organization != null)
                    return Ok(organization);
                else
                    return NotFound("Организация с заданным id не найдена");
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при получении организации с id = {id}: {ex.Message}");
                return StatusCode(500, "Ошибка при получении организации");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrganizationAsync([FromBody] Organization organization)
        {
            if (organization == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await organizationRepository.AddAsync(organization);
                return CreatedAtAction(nameof(GetOrganizationAsync), new { id = organization.Id }, organization);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при добавлении Organization в БД: {ex.Message}");
                return StatusCode(500, "Ошибка при создании организации");
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrganizationAsync([FromBody] Organization organizationUpdate)
        {
            if (organizationUpdate == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await organizationRepository.UpdateAsync(organizationUpdate);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при обновлении Organization: {ex.Message}");
                return StatusCode(500, "Ошибка при обновлении организации");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganizationAsync(int id)
        {
            try
            {
                var organization = await organizationRepository.GetByIdAsync(id);
                if (organization == null)
                    return NotFound("Организации с таким id не существует");
                await organizationRepository.DeleteAsync(organization);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при удалении Organization: {ex.Message}");
                return StatusCode(500, "Организация не была удалена");
            }
        }
    }
}
