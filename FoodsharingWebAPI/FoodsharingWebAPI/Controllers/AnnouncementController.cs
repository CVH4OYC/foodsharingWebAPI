using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly IRepository<Announcement> announcementRepository;
        private readonly ILogger<AnnouncementController> logger;
        public AnnouncementController(IRepository<Announcement> announcementRepository, ILogger<AnnouncementController> logger)
        {
            this.announcementRepository = announcementRepository;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<List<Announcement>>> GetAllAsync()
        {
            try
            {
                var Announcements = await announcementRepository.GetAllAsync();
                if (Announcements != null)
                    return Ok(Announcements);
                else
                    return NotFound("Объявлений не существует");
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при получении списка Announcement: {ex.Message}");
                return StatusCode(500, "Ошибка при получении списка объявлений");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Announcement>> GetAnnouncementAsync(int id)
        {
            try
            {
                var Announcement = await announcementRepository.GetByIdAsync(id);
                if (Announcement != null)
                    return Ok(Announcement);
                else
                    return NotFound("Объявление с заданным id не найдено");
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при получении объявления с id = {id}: {ex.Message}");
                return StatusCode(500, "Ошибка при получении объявления");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateAnnouncementAsync([FromBody] Announcement Announcement)
        {
            if (Announcement == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await announcementRepository.AddAsync(Announcement);
                return CreatedAtAction(nameof(GetAnnouncementAsync), new { id = Announcement.Id }, Announcement);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при добавлении Announcement в БД: {ex.Message}");
                return StatusCode(500, "Ошибка при создании объявления");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnnouncementAsync(int id, [FromBody] Announcement AnnouncementUpdate)
        {
            if (AnnouncementUpdate == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                var Announcement = await announcementRepository.GetByIdAsync(id);
                if (Announcement == null)
                    return NotFound("Объявления с таким id не существует");

                if(!string.IsNullOrEmpty(AnnouncementUpdate.Title))
                    Announcement.Title = AnnouncementUpdate.Title;
                if (!string.IsNullOrEmpty(AnnouncementUpdate.Description))
                    Announcement.Description = AnnouncementUpdate.Description;
                if (!string.IsNullOrEmpty(AnnouncementUpdate.Image))
                    Announcement.Image = AnnouncementUpdate.Image;
                Announcement.AddressId = AnnouncementUpdate.AddressId;
                Announcement.CategoryId = AnnouncementUpdate.CategoryId;
                Announcement.ExpirationDate = AnnouncementUpdate.ExpirationDate;

                await announcementRepository.UpdateAsync(Announcement);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при обновлении Announcement: {ex.Message}");
                return StatusCode(500, "Ошибка при обновлении объявления");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncementAsync(int id)
        {
            try
            {
                var Announcement = await announcementRepository.GetByIdAsync(id);
                if (Announcement == null)
                    return NotFound("Объявления с таким id не существует");
                await announcementRepository.DeleteAsync(Announcement);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при удалении Announcement: {ex.Message}");
                return StatusCode(500, "Объявление не было удалёно");
            }
        }
    }
}
