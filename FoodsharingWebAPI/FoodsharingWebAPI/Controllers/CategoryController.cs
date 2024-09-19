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
        private readonly IRepository<Category> CategoryRepository;
        private readonly ILogger<CategoryController> logger;
        public CategoryController(IRepository<Category> CategoryRepository, ILogger<CategoryController> logger)
        {
            this.CategoryRepository = CategoryRepository;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAll()
        {
            try
            {
                var Categorys = await CategoryRepository.GetAll();
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
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            try
            {
                var Category = await CategoryRepository.GetById(id);
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
        public async Task<IActionResult> CreateCategory([FromBody] Category Category)
        {
            if (Category == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await CategoryRepository.Add(Category);
                return CreatedAtAction(nameof(GetCategory), new { id = Category.Id }, Category);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при добавлении Category в БД: {ex.Message}");
                return StatusCode(500, "Ошибка при создании категории");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category CategoryUpdate)
        {
            if (CategoryUpdate == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                var Category = await CategoryRepository.GetById(id);
                if (Category == null)
                    return NotFound("Категории с таким id не существует");
                
                if(!string.IsNullOrEmpty(CategoryUpdate.Name))
                    Category.Name = CategoryUpdate.Name;

                await CategoryRepository.Update(Category);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при обновлении Category: {ex.Message}");
                return StatusCode(500, "Ошибка при обновлении категории");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var Category = await CategoryRepository.GetById(id);
                if (Category == null)
                    return NotFound("Категории с таким id не существует");
                await CategoryRepository.Delete(Category);
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
