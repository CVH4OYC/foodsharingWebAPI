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
                var Organizations = await organizationRepository.GetAllAsync();
                if (Organizations != null)
                    return Ok(Organizations);
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
                var Organization = await organizationRepository.GetByIdAsync(id);
                if (Organization != null)
                    return Ok(Organization);
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
        public async Task<IActionResult> CreateOrganizationAsync([FromBody] Organization Organization)
        {
            if (Organization == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await organizationRepository.AddAsync(Organization);
                return CreatedAtAction(nameof(GetOrganizationAsync), new { id = Organization.Id }, Organization);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при добавлении Organization в БД: {ex.Message}");
                return StatusCode(500, "Ошибка при создании организации");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrganizationAsync(int id, [FromBody] Organization OrganizationUpdate)
        {
            if (OrganizationUpdate == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                var Organization = await organizationRepository.GetByIdAsync(id);
                if (Organization == null)
                    return NotFound("Организации с таким id не существует");

                if (!string.IsNullOrEmpty(OrganizationUpdate.Name))
                    Organization.Name = OrganizationUpdate.Name;
                if (!string.IsNullOrEmpty(OrganizationUpdate.Phone))
                    Organization.Phone = OrganizationUpdate.Phone;
                if (!string.IsNullOrEmpty(OrganizationUpdate.Email))
                    Organization.Email = OrganizationUpdate.Email;
                if (!string.IsNullOrEmpty(OrganizationUpdate.Website))
                    Organization.Website = OrganizationUpdate.Website;
                if (!string.IsNullOrEmpty(OrganizationUpdate.Description))
                    Organization.Description = OrganizationUpdate.Description;
                Organization.AddressId = OrganizationUpdate.AddressId;

                await organizationRepository.UpdateAsync(Organization);
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
                var Organization = await organizationRepository.GetByIdAsync(id);
                if (Organization == null)
                    return NotFound("Организации с таким id не существует");
                await organizationRepository.DeleteAsync(Organization);
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
