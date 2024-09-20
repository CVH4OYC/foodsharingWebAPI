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
        public async Task<ActionResult<List<Message>>> GetAll()
        {
            try
            {
                var Messages = await messageRepository.GetAll();
                if (Messages != null)
                    return Ok(Messages);
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
        public async Task<ActionResult<Message>> GetMessage(int id)
        {
            try
            {
                var Message = await messageRepository.GetById(id);
                if (Message != null)
                    return Ok(Message);
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
        public async Task<IActionResult> CreateMessage([FromBody] Message Message)
        {
            if (Message == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await messageRepository.Add(Message);
                return CreatedAtAction(nameof(GetMessage), new { id = Message.Id }, Message);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при добавлении Message в БД: {ex.Message}");
                return StatusCode(500, "Ошибка при создании сообщения");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMessage(int id, [FromBody] Message MessageUpdate)
        {
            if (MessageUpdate == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                var Message = await messageRepository.GetById(id);
                if (Message == null)
                    return NotFound("Сообщения с таким id не существует");

                if (!string.IsNullOrEmpty(MessageUpdate.Text))
                    Message.Text = MessageUpdate.Text;
                if(!string.IsNullOrEmpty(MessageUpdate.Image))
                    Message.Image = MessageUpdate.Image;
                if(!string.IsNullOrEmpty(MessageUpdate.File))
                    Message.File = MessageUpdate.File;

                Message.StatusId = MessageUpdate.StatusId;

                await messageRepository.Update(Message);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при обновлении Message: {ex.Message}");
                return StatusCode(500, "Ошибка при обновлении сообщения");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            try
            {
                var Message = await messageRepository.GetById(id);
                if (Message == null)
                    return NotFound("Сообщения с таким id не существует");
                await messageRepository.Delete(Message);
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
