using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IRepository<Message> messageRepository;
        private readonly ILogger<MessageController> logger;
        public MessageController(IRepository<Message> messageRepository, ILogger<MessageController> logger)
        {
            this.messageRepository = messageRepository;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<List<Message>>> GetAllAsync()
        {
            try
            {
                var messages = await messageRepository.GetAllAsync();
                if (messages != null)
                    return Ok(messages);
                else
                    return NotFound("Сообщений не существует");
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при получении списка Message: {ex.Message}");
                return StatusCode(500, "Ошибка при получении списка сообщений");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessageAsync(int id)
        {
            try
            {
                var message = await messageRepository.GetByIdAsync(id);
                if (message != null)
                    return Ok(message);
                else
                    return NotFound("Сообщение с заданным id не найдено");
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при получении сообщения с id = {id}: {ex.Message}");
                return StatusCode(500, "Ошибка при получении сообщения");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateMessageAsync([FromBody] Message message)
        {
            if (message == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await messageRepository.AddAsync(message);
                return CreatedAtAction(nameof(GetMessageAsync), new { id = message.Id }, message);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при добавлении Message в БД: {ex.Message}");
                return StatusCode(500, "Ошибка при создании сообщения");
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateMessageAsync([FromBody] Message messageUpdate)
        {
            if (messageUpdate == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await messageRepository.UpdateAsync(messageUpdate);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при обновлении Message: {ex.Message}");
                return StatusCode(500, "Ошибка при обновлении сообщения");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessageAsync(int id)
        {
            try
            {
                var message = await messageRepository.GetByIdAsync(id);
                if (message == null)
                    return NotFound("Сообщения с таким id не существует");
                await messageRepository.DeleteAsync(message);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при удалении Message: {ex.Message}");
                return StatusCode(500, "Сообщение не было удалено");
            }
        }
    }
}
