using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    /// <summary>
    /// Базовый контроллер
    /// </summary>
    /// <typeparam name="T">Тип сущности, с которой работает контроллер</typeparam>
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase where T : class
    {
        private readonly IRepository<T> repository;
        private readonly ILogger<BaseController<T>> logger;
        /// <summary>
        /// Конструктор базового контроллера
        /// </summary>
        /// <param name="repository">Экземпляр универсального репозитория для доступа к сущностям</param>
        /// <param name="logger">Логгер для записи информации</param>
        public BaseController(IRepository<T> repository, ILogger<BaseController<T>> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }
        /// <summary>
        /// Метод для получения всех сущностей заданного типа
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>   
        /// Коллекция сущностей <br/>
        /// Код состояния:<br/>
        /// - 200 OK: если сущности найдены<br/>
        /// - 404 Not Found: если сущности не найдены</returns>
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
        /// <param name="id">id сущности</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Найденная сущность<br/>
        /// Код состояния:<br/>
        /// - 200 OK: если сущность найдена<br/>
        /// - 404 Not Found: если сущность не найдена<br/>
        /// </returns>
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
        /// <param name="entity">Создаваемая сущность</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>
        /// Созданная сущность<br/>
        /// Код состояния:<br/>
        /// - 201 Created: если сущность успешно создана<br/>
        /// - 400 Bad Request: если тело запроса пустое.
        /// </returns>

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
        /// <param name="entityUpdate">Обновляемая сущность</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>
        /// Код состояния: <br/>
        /// - 200 OK: если сущность успешно обновлена<br/>
        /// - 400 Bad Request: если тело запроса пустое<br/>
        /// </returns>
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
        /// <param name="entityDelete">Удаляемая сущность</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>
        /// Код состояния:<br/>
        /// - 200 OK: если сущность успешно удалена<br/>
        /// - 400 Bad Request: если тело запроса пустое
        /// </returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody] T entityDelete, CancellationToken cancellationToken)
        {
            if (entityDelete == null)
                return BadRequest("Тело запроса пустое");
            await repository.DeleteAsync(entityDelete, cancellationToken);
            return Ok();
        }
    }
}
