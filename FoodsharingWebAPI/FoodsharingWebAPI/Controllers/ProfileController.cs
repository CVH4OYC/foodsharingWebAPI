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
        private readonly IRepository<Profile> ProfileRepository;
        private readonly ILogger<ProfileController> logger;
        public ProfileController(IRepository<Profile> ProfileRepository, ILogger<ProfileController> logger)
        {
            this.ProfileRepository = ProfileRepository;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<List<Profile>>> GetAll()
        {
            try
            {
                var Profiles = await ProfileRepository.GetAll();
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
        public async Task<ActionResult<Profile>> GetProfile(int id)
        {
            try
            {
                var Profile = await ProfileRepository.GetById(id);
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
        public async Task<IActionResult> CreateProfile([FromBody] Profile Profile)
        {
            if (Profile == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await ProfileRepository.Add(Profile);
                return CreatedAtAction(nameof(GetProfile), new { id = Profile.Id }, Profile);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при добавлении Profile в БД: {ex.Message}");
                return StatusCode(500, "Ошибка при создании профиля");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile(int id, [FromBody] Profile ProfileUpdate)
        {
            if (ProfileUpdate == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                var Profile = await ProfileRepository.GetById(id);
                if (Profile == null)
                    return NotFound("Профиля с таким id не существует");
                Profile.FirstName = ProfileUpdate.FirstName;
                if (!string.IsNullOrEmpty(ProfileUpdate.LastName)) 
                    Profile.LastName = ProfileUpdate.LastName;
                if (!string.IsNullOrEmpty(ProfileUpdate.Image))
                    Profile.Image = ProfileUpdate.Image;
                if (!string.IsNullOrEmpty(ProfileUpdate.Bio))
                    Profile.Bio = ProfileUpdate.Bio;

                await ProfileRepository.Update(Profile);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при обновлении Profile: {ex.Message}");
                return StatusCode(500, "Ошибка при обновлении профиля");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            try
            {
                var Profile = await ProfileRepository.GetById(id);
                if (Profile == null)
                    return NotFound("Профиля с таким id не существует");
                await ProfileRepository.Delete(Profile);
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
