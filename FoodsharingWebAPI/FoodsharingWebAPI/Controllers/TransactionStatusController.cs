using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionStatusController : ControllerBase
    {
        private readonly IRepository<TransactionStatus> transactionStatusRepository;
        private readonly ILogger<TransactionStatusController> logger;
        public TransactionStatusController(IRepository<TransactionStatus> transactionStatusRepository, ILogger<TransactionStatusController> logger)
        {
            this.transactionStatusRepository = transactionStatusRepository;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<List<TransactionStatus>>> GetAllAsync()
        {
            try
            {
                var TransactionStatuss = await transactionStatusRepository.GetAllAsync();
                if (TransactionStatuss != null)
                    return Ok(TransactionStatuss);
                else
                    return NotFound("Статусов транзакций не существует");
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при получении списка TransactionStatus: {ex.Message}");
                return StatusCode(500, "Ошибка при получении списка статусов транзакций");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionStatus>> GetTransactionStatusAsync(int id)
        {
            try
            {
                var TransactionStatus = await transactionStatusRepository.GetByIdAsync(id);
                if (TransactionStatus != null)
                    return Ok(TransactionStatus);
                else
                    return NotFound("Статус транзакции с заданным id не найден");
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при получении статуса транзакции с id = {id}: {ex.Message}");
                return StatusCode(500, "Ошибка при получении статуса транзакции");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateTransactionStatusAsync([FromBody] TransactionStatus TransactionStatus)
        {
            if (TransactionStatus == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await transactionStatusRepository.AddAsync(TransactionStatus);
                return CreatedAtAction(nameof(GetTransactionStatusAsync), new { id = TransactionStatus.Id }, TransactionStatus);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при добавлении TransactionStatus в БД: {ex.Message}");
                return StatusCode(500, "Ошибка при создании статуса транзакции");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransactionStatusAsync(int id, [FromBody] TransactionStatus TransactionStatusUpdate)
        {
            if (TransactionStatusUpdate == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                var TransactionStatus = await transactionStatusRepository.GetByIdAsync(id);
                if (TransactionStatus == null)
                    return NotFound("Статуса транзакции с таким id не существует");
                if (!string.IsNullOrEmpty(TransactionStatusUpdate.Name))
                    TransactionStatus.Name = TransactionStatusUpdate.Name;
                await transactionStatusRepository.UpdateAsync(TransactionStatus);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при обновлении TransactionStatus: {ex.Message}");
                return StatusCode(500, "Ошибка при обновлении статуса транзакции");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransactionStatusAsync(int id)
        {
            try
            {
                var TransactionStatus = await transactionStatusRepository.GetByIdAsync(id);
                if (TransactionStatus == null)
                    return NotFound("Статуса транзакции с таким id не существует");
                await transactionStatusRepository.DeleteAsync(TransactionStatus);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при удалении TransactionStatus: {ex.Message}");
                return StatusCode(500, "Статус транзакции не был удалён");
            }
        }
    }
}
