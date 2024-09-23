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
                var announcements = await announcementRepository.GetAllAsync();
                if (announcements != null)
                    return Ok(announcements);
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
                var announcement = await announcementRepository.GetByIdAsync(id);
                if (announcement != null)
                    return Ok(announcement);
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
        public async Task<IActionResult> CreateAnnouncementAsync([FromBody] Announcement announcement)
        {
            if (announcement == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await announcementRepository.AddAsync(announcement);
                return CreatedAtAction(nameof(GetAnnouncementAsync), new { id = announcement.Id }, announcement);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при добавлении Announcement в БД: {ex.Message}");
                return StatusCode(500, "Ошибка при создании объявления");
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAnnouncementAsync([FromBody] Announcement announcementUpdate)
        {
            if (announcementUpdate == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await announcementRepository.UpdateAsync(announcementUpdate);
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
                var announcement = await announcementRepository.GetByIdAsync(id);
                if (announcement == null)
                    return NotFound("Объявления с таким id не существует");
                await announcementRepository.DeleteAsync(announcement);
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
