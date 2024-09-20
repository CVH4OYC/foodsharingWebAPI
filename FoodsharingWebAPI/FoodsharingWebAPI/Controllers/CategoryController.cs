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
                var Categorys = await categoryRepository.GetAllAsync();
                if (Categorys != null)
                    return Ok(Categorys);
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
                var Category = await categoryRepository.GetByIdAsync(id);
                if (Category != null)
                    return Ok(Category);
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
        public async Task<IActionResult> CreateCategoryAsync([FromBody] Category Category)
        {
            if (Category == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await categoryRepository.AddAsync(Category);
                return CreatedAtAction(nameof(GetCategoryAsync), new { id = Category.Id }, Category);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при добавлении Category в БД: {ex.Message}");
                return StatusCode(500, "Ошибка при создании категории");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryAsync(int id, [FromBody] Category CategoryUpdate)
        {
            if (CategoryUpdate == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                var Category = await categoryRepository.GetByIdAsync(id);
                if (Category == null)
                    return NotFound("Категории с таким id не существует");
                
                if(!string.IsNullOrEmpty(CategoryUpdate.Name))
                    Category.Name = CategoryUpdate.Name;

                await categoryRepository.UpdateAsync(Category);
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
                var Category = await categoryRepository.GetByIdAsync(id);
                if (Category == null)
                    return NotFound("Категории с таким id не существует");
                await categoryRepository.DeleteAsync(Category);
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
