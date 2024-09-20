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
                var Representatives = await representativeRepository.GetAllAsync();
                if (Representatives != null)
                    return Ok(Representatives);
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
                var Representative = await representativeRepository.GetByIdAsync(id);
                if (Representative != null)
                    return Ok(Representative);
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
        public async Task<IActionResult> CreateRepresentativeAsync([FromBody] Representative Representative)
        {
            if (Representative == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await representativeRepository.AddAsync(Representative);
                return CreatedAtAction(nameof(GetRepresentativeAsync), new { id = Representative.Id }, Representative);
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
                var Representative = await representativeRepository.GetByIdAsync(id);
                if (Representative == null)
                    return NotFound("Представителя организации с таким id не существует");
                await representativeRepository.DeleteAsync(Representative);
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
