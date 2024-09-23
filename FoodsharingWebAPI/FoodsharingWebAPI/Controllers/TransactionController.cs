using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IRepository<Transaction> transactionRepository;
        private readonly ILogger<TransactionController> logger;
        public TransactionController(IRepository<Transaction> transactionRepository, ILogger<TransactionController> logger)
        {
            this.transactionRepository = transactionRepository;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<List<Transaction>>> GetAllAsync()
        {
            try
            {
                var transactions = await transactionRepository.GetAllAsync();
                if (transactions != null)
                    return Ok(transactions);
                else
                    return NotFound("Транзакций не существует");
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при получении списка Transaction: {ex.Message}");
                return StatusCode(500, "Ошибка при получении списка транзакций");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransactionAsync(int id)
        {
            try
            {
                var transaction = await transactionRepository.GetByIdAsync(id);
                if (transaction != null)
                    return Ok(transaction);
                else
                    return NotFound("Транзакция с заданным id не найдена");
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при получении транзакции с id = {id}: {ex.Message}");
                return StatusCode(500, "Ошибка при получении транзакции");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateTransactionAsync([FromBody] Transaction transaction)
        {
            if (transaction == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await transactionRepository.AddAsync(transaction);
                return CreatedAtAction(nameof(GetTransactionAsync), new { id = transaction.Id }, transaction);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при добавлении Transaction в БД: {ex.Message}");
                return StatusCode(500, "Ошибка при создании транзакции");
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTransactionAsync([FromBody] Transaction transactionUpdate)
        {
            if (transactionUpdate == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await transactionRepository.UpdateAsync(transactionUpdate);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при обновлении Transaction: {ex.Message}");
                return StatusCode(500, "Ошибка при обновлении транзакции");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransactionAsync(int id)
        {
            try
            {
                var transaction = await transactionRepository.GetByIdAsync(id);
                if (transaction == null)
                    return NotFound("Транзакции с таким id не существует");
                await transactionRepository.DeleteAsync(transaction);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при удалении Transaction: {ex.Message}");
                return StatusCode(500, "Транзакция не была удалена");
            }
        }
    }
}
