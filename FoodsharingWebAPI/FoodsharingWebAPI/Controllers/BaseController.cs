using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    /// <summary>
    /// Общий класс для контроллеров
    /// </summary>
    
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase where T : class
    {
        private readonly IRepository<T> repository;
        private readonly ILogger<BaseController<T>> logger;
        public BaseController(IRepository<T> repository, ILogger<BaseController<T>> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }
        /// <summary>
        /// Метод для получения всех сущностей заданного типа
        /// </summary>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<T>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var entities = await repository.GetAllAsync(cancellationToken);
            if (entities != null)
                return Ok(entities);
            else
                return NotFound("Не существует");
        }
        /// <summary>
        /// Метод для получения сущности заданного типа по его id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<T>> GetEntityAsync(int id, CancellationToken cancellationToken)
        {
            var entities = await repository.GetByIdAsync(id, cancellationToken);
            if (entities != null)
                return Ok(entities);
            else
                return NotFound("Объект с заданным id не найден");
        }
        /// <summary>
        /// Метод для создания сущности
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] T entity, CancellationToken cancellationToken)
        {
            if (entity == null)
                return BadRequest("Тело запроса пустое");

            await repository.AddAsync(entity, cancellationToken);
            return CreatedAtAction(nameof(GetEntityAsync), new { id = entity.GetType().GetProperty("Id")?.GetValue(entity)}, entity);

        }
        /// <summary>
        /// Метод для обновления сущности
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] T entityUpdate, CancellationToken cancellationToken)
        {
            if (entityUpdate == null)
                return BadRequest("Тело запроса пустое");

            await repository.UpdateAsync(entityUpdate, cancellationToken);
            return Ok();

        }
        /// <summary>
        /// Метод для удаления сущности
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody] T entityDelete, CancellationToken cancellationToken)
        {
            await repository.DeleteAsync(entityDelete, cancellationToken);
            return Ok();
        }
    }
}
