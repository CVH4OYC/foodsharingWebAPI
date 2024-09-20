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
                var Profiles = await profileRepository.GetAllAsync();
                if (Profiles != null)
                    return Ok(Profiles);
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
                var Profile = await profileRepository.GetByIdAsync(id);
                if (Profile != null)
                    return Ok(Profile);
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
        public async Task<IActionResult> CreateProfileAsync([FromBody] Profile Profile)
        {
            if (Profile == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await profileRepository.AddAsync(Profile);
                return CreatedAtAction(nameof(GetProfileAsync), new { id = Profile.Id }, Profile);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при добавлении Profile в БД: {ex.Message}");
                return StatusCode(500, "Ошибка при создании профиля");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfileAsync(int id, [FromBody] Profile ProfileUpdate)
        {
            if (ProfileUpdate == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                var Profile = await profileRepository.GetByIdAsync(id);
                if (Profile == null)
                    return NotFound("Профиля с таким id не существует");
                Profile.FirstName = ProfileUpdate.FirstName;
                if (!string.IsNullOrEmpty(ProfileUpdate.LastName)) 
                    Profile.LastName = ProfileUpdate.LastName;
                if (!string.IsNullOrEmpty(ProfileUpdate.Image))
                    Profile.Image = ProfileUpdate.Image;
                if (!string.IsNullOrEmpty(ProfileUpdate.Bio))
                    Profile.Bio = ProfileUpdate.Bio;

                await profileRepository.UpdateAsync(Profile);
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
                var Profile = await profileRepository.GetByIdAsync(id);
                if (Profile == null)
                    return NotFound("Профиля с таким id не существует");
                await profileRepository.DeleteAsync(Profile);
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
