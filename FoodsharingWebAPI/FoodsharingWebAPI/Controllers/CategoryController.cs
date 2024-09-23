using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IRepository<Category> categoryRepository;
        private readonly ILogger<CategoryController> logger;
        public CategoryController(IRepository<Category> categoryRepository, ILogger<CategoryController> logger)
        {
            this.categoryRepository = categoryRepository;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAllAsync()
        {
            try
            {
                var categorys = await categoryRepository.GetAllAsync();
                if (categorys != null)
                    return Ok(categorys);
                else
                    return NotFound("Категорий не существует");
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при получении списка Category: {ex.Message}");
                return StatusCode(500, "Ошибка при получении списка категорий");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryAsync(int id)
        {
            try
            {
                var category = await categoryRepository.GetByIdAsync(id);
                if (category != null)
                    return Ok(category);
                else
                    return NotFound("Категория с заданным id не найдена");
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при получении роли с id = {id}: {ex.Message}");
                return StatusCode(500, "Ошибка при получении категории");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] Category category)
        {
            if (category == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await categoryRepository.AddAsync(category);
                return CreatedAtAction(nameof(GetCategoryAsync), new { id = category.Id }, category);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при добавлении Category в БД: {ex.Message}");
                return StatusCode(500, "Ошибка при создании категории");
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategoryAsync([FromBody] Category categoryUpdate)
        {
            if (categoryUpdate == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await categoryRepository.UpdateAsync(categoryUpdate);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при обновлении Category: {ex.Message}");
                return StatusCode(500, "Ошибка при обновлении категории");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            try
            {
                await categoryRepository.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при удалении Category: {ex.Message}");
                return StatusCode(500, "Категория не была удалёна");
            }
        }
    }
}
