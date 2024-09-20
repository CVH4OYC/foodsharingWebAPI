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
        private readonly IRepository<MessageStatus> MessageStatusRepository;
        private readonly ILogger<MessageStatusController> logger;
        public MessageStatusController(IRepository<MessageStatus> MessageStatusRepository, ILogger<MessageStatusController> logger)
        {
            this.MessageStatusRepository = MessageStatusRepository;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<List<MessageStatus>>> GetAll()
        {
            try
            {
                var MessageStatuss = await MessageStatusRepository.GetAll();
                if (MessageStatuss != null)
                    return Ok(MessageStatuss);
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
        public async Task<ActionResult<MessageStatus>> GetMessageStatus(int id)
        {
            try
            {
                var MessageStatus = await MessageStatusRepository.GetById(id);
                if (MessageStatus != null)
                    return Ok(MessageStatus);
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
        public async Task<IActionResult> CreateMessageStatus([FromBody] MessageStatus MessageStatus)
        {
            if (MessageStatus == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await MessageStatusRepository.Add(MessageStatus);
                return CreatedAtAction(nameof(GetMessageStatus), new { id = MessageStatus.Id }, MessageStatus);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при добавлении MessageStatus в БД: {ex.Message}");
                return StatusCode(500, "Ошибка при создании статуса сообщения");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMessageStatus(int id, [FromBody] MessageStatus MessageStatusUpdate)
        {
            if (MessageStatusUpdate == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                var MessageStatus = await MessageStatusRepository.GetById(id);
                if (MessageStatus == null)
                    return NotFound("Статуса сообщения с таким id не существует");

                if (!string.IsNullOrEmpty(MessageStatusUpdate.Name))
                    MessageStatus.Name = MessageStatusUpdate.Name;

                await MessageStatusRepository.Update(MessageStatus);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при обновлении MessageStatus: {ex.Message}");
                return StatusCode(500, "Ошибка при обновлении статуса сообщения");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessageStatus(int id)
        {
            try
            {
                var MessageStatus = await MessageStatusRepository.GetById(id);
                if (MessageStatus == null)
                    return NotFound("Статуса сообщения с таким id не существует");
                await MessageStatusRepository.Delete(MessageStatus);
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
