using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IRepository<Profile> profileRepository;
        private readonly ILogger<ProfileController> logger;
        public ProfileController(IRepository<Profile> profileRepository, ILogger<ProfileController> logger)
        {
            this.profileRepository = profileRepository;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<List<Profile>>> GetAllAsync()
        {
            try
            {
                var profiles = await profileRepository.GetAllAsync();
                if (profiles != null)
                    return Ok(profiles);
                else
                    return NotFound("Профилей не существует");
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при получении списка Profile: {ex.Message}");
                return StatusCode(500, "Ошибка при получении списка профилей");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Profile>> GetProfileAsync(int id)
        {
            try
            {
                var profile = await profileRepository.GetByIdAsync(id);
                if (profile != null)
                    return Ok(profile);
                else
                    return NotFound("Профиль с заданным id не найден");
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при получении профиля с id = {id}: {ex.Message}");
                return StatusCode(500, "Ошибка при получении профиля");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateProfileAsync([FromBody] Profile profile)
        {
            if (profile == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await profileRepository.AddAsync(profile);
                return CreatedAtAction(nameof(GetProfileAsync), new { id = profile.Id }, profile);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при добавлении Profile в БД: {ex.Message}");
                return StatusCode(500, "Ошибка при создании профиля");
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProfileAsync([FromBody] Profile profileUpdate)
        {
            if (profileUpdate == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await profileRepository.UpdateAsync(profileUpdate);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при обновлении Profile: {ex.Message}");
                return StatusCode(500, "Ошибка при обновлении профиля");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfileAsync(int id)
        {
            try
            {
                await profileRepository.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при удалении Profile: {ex.Message}");
                return StatusCode(500, "Профиль не был удалён");
            }
        }
    }
}
