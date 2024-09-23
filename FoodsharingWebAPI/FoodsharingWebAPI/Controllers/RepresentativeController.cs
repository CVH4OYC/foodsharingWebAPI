using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepresentativeController : ControllerBase
    {
        private readonly IRepository<Representative> representativeRepository;
        private readonly ILogger<RepresentativeController> logger;
        public RepresentativeController(IRepository<Representative> representativeRepository, ILogger<RepresentativeController> logger)
        {
            this.representativeRepository = representativeRepository;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<List<Representative>>> GetAllAsync()
        {
            try
            {
                var representatives = await representativeRepository.GetAllAsync();
                if (representatives != null)
                    return Ok(representatives);
                else
                    return NotFound("Представителей организаций не существует");
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при получении списка Representative: {ex.Message}");
                return StatusCode(500, "Ошибка при получении списка представителей организаций");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Representative>> GetRepresentativeAsync(int id)
        {
            try
            {
                var representative = await representativeRepository.GetByIdAsync(id);
                if (representative != null)
                    return Ok(representative);
                else
                    return NotFound("Представитель организации с заданным id не найден");
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при получении представителя организации с id = {id}: {ex.Message}");
                return StatusCode(500, "Ошибка при получении представителя организации");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateRepresentativeAsync([FromBody] Representative representative)
        {
            if (representative == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await representativeRepository.AddAsync(representative);
                return CreatedAtAction(nameof(GetRepresentativeAsync), new { id = representative.Id }, representative);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при добавлении Representative в БД: {ex.Message}");
                return StatusCode(500, "Ошибка при создании представителя организации");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRepresentativeAsync(int id)
        {
            try
            {
                var representative = await representativeRepository.GetByIdAsync(id);
                if (representative == null)
                    return NotFound("Представителя организации с таким id не существует");
                await representativeRepository.DeleteAsync(representative);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при удалении Representative: {ex.Message}");
                return StatusCode(500, "Представитель организации не был удалён");
            }
        }
    }
}
