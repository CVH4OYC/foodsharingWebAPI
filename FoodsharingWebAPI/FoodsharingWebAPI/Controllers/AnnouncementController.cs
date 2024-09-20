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
        public async Task<ActionResult<List<Announcement>>> GetAll()
        {
            try
            {
                var Announcements = await announcementRepository.GetAll();
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
        public async Task<ActionResult<Announcement>> GetAnnouncement(int id)
        {
            try
            {
                var Announcement = await announcementRepository.GetById(id);
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
        public async Task<IActionResult> CreateAnnouncement([FromBody] Announcement Announcement)
        {
            if (Announcement == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await announcementRepository.Add(Announcement);
                return CreatedAtAction(nameof(GetAnnouncement), new { id = Announcement.Id }, Announcement);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при добавлении Announcement в БД: {ex.Message}");
                return StatusCode(500, "Ошибка при создании объявления");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnnouncement(int id, [FromBody] Announcement AnnouncementUpdate)
        {
            if (AnnouncementUpdate == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                var Announcement = await announcementRepository.GetById(id);
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

                await announcementRepository.Update(Announcement);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при обновлении Announcement: {ex.Message}");
                return StatusCode(500, "Ошибка при обновлении объявления");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            try
            {
                var Announcement = await announcementRepository.GetById(id);
                if (Announcement == null)
                    return NotFound("Объявления с таким id не существует");
                await announcementRepository.Delete(Announcement);
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
