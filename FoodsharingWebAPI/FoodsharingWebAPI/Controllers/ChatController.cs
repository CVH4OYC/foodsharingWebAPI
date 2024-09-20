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
                var Chats = await chatRepository.GetAllAsync();
                if (Chats != null)
                    return Ok(Chats);
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
                var Chat = await chatRepository.GetByIdAsync(id);
                if (Chat != null)
                    return Ok(Chat);
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
        public async Task<IActionResult> CreateChatAsync([FromBody] Chat Chat)
        {
            if (Chat == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await chatRepository.AddAsync(Chat);
                return CreatedAtAction(nameof(GetChatAsync), new { id = Chat.Id }, Chat);
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
                var Chat = await chatRepository.GetByIdAsync(id);
                if (Chat == null)
                    return NotFound("Чата с таким id не существует");
                await chatRepository.DeleteAsync(Chat);
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
