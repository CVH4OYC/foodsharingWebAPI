using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageStatusController : ControllerBase
    {
        private readonly IRepository<MessageStatus> messageStatusRepository;
        private readonly ILogger<MessageStatusController> logger;
        public MessageStatusController(IRepository<MessageStatus> messageStatusRepository, ILogger<MessageStatusController> logger)
        {
            this.messageStatusRepository = messageStatusRepository;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<List<MessageStatus>>> GetAllAsync()
        {
            try
            {
                var messageStatuss = await messageStatusRepository.GetAllAsync();
                if (messageStatuss != null)
                    return Ok(messageStatuss);
                else
                    return NotFound("Статусов сообщений не существует");
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при получении списка MessageStatus: {ex.Message}");
                return StatusCode(500, "Ошибка при получении списка статусов сообщений");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MessageStatus>> GetMessageStatusAsync(int id)
        {
            try
            {
                var messageStatus = await messageStatusRepository.GetByIdAsync(id);
                if (messageStatus != null)
                    return Ok(messageStatus);
                else
                    return NotFound("Статус сообщения с заданным id не найдена");
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при получении роли с id = {id}: {ex.Message}");
                return StatusCode(500, "Ошибка при получении статуса сообщения");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateMessageStatusAsync([FromBody] MessageStatus messageStatus)
        {
            if (messageStatus == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await messageStatusRepository.AddAsync(messageStatus);
                return CreatedAtAction(nameof(GetMessageStatusAsync), new { id = messageStatus.Id }, messageStatus);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при добавлении MessageStatus в БД: {ex.Message}");
                return StatusCode(500, "Ошибка при создании статуса сообщения");
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateMessageStatusAsync([FromBody] MessageStatus messageStatusUpdate)
        {
            if (messageStatusUpdate == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await messageStatusRepository.UpdateAsync(messageStatusUpdate);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при обновлении MessageStatus: {ex.Message}");
                return StatusCode(500, "Ошибка при обновлении статуса сообщения");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessageStatusAsync(int id)
        {
            try
            {
                var messageStatus = await messageStatusRepository.GetByIdAsync(id);
                if (messageStatus == null)
                    return NotFound("Статуса сообщения с таким id не существует");
                await messageStatusRepository.DeleteAsync(messageStatus);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при удалении MessageStatus: {ex.Message}");
                return StatusCode(500, "Статус сообщения не был удалён");
            }
        }
    }
}
