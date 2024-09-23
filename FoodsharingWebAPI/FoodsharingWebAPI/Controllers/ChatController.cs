using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IRepository<Chat> chatRepository;
        private readonly ILogger<ChatController> logger;
        public ChatController(IRepository<Chat> chatRepository, ILogger<ChatController> logger)
        {
            this.chatRepository = chatRepository;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<List<Chat>>> GetAllAsync()
        {
            try
            {
                var chats = await chatRepository.GetAllAsync();
                if (chats != null)
                    return Ok(chats);
                else
                    return NotFound("Чатов не существует");
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при получении списка Chat: {ex.Message}");
                return StatusCode(500, "Ошибка при получении списка чатов");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Chat>> GetChatAsync(int id)
        {
            try
            {
                var chat = await chatRepository.GetByIdAsync(id);
                if (chat != null)
                    return Ok(chat);
                else
                    return NotFound("Чат с заданным id не найден");
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при получении чата с id = {id}: {ex.Message}");
                return StatusCode(500, "Ошибка при получении чата");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateChatAsync([FromBody] Chat chat)
        {
            if (chat == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await chatRepository.AddAsync(chat);
                return CreatedAtAction(nameof(GetChatAsync), new { id = chat.Id }, chat);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при добавлении Chat в БД: {ex.Message}");
                return StatusCode(500, "Ошибка при создании чата");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChatAsync(int id)
        {
            try
            {
                var chat = await chatRepository.GetByIdAsync(id);
                if (chat == null)
                    return NotFound("Чата с таким id не существует");
                await chatRepository.DeleteAsync(chat);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при удалении Chat: {ex.Message}");
                return StatusCode(500, "Чат не был удалён");
            }
        }
    }
}
