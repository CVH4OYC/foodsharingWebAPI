﻿using FoodsharingWebAPI.Interfaces;
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
                var Transactions = await transactionRepository.GetAllAsync();
                if (Transactions != null)
                    return Ok(Transactions);
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
                var Transaction = await transactionRepository.GetByIdAsync(id);
                if (Transaction != null)
                    return Ok(Transaction);
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
        public async Task<IActionResult> CreateTransactionAsync([FromBody] Transaction Transaction)
        {
            if (Transaction == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await transactionRepository.AddAsync(Transaction);
                return CreatedAtAction(nameof(GetTransactionAsync), new { id = Transaction.Id }, Transaction);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при добавлении Transaction в БД: {ex.Message}");
                return StatusCode(500, "Ошибка при создании транзакции");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransactionAsync(int id, [FromBody] Transaction TransactionUpdate)
        {
            if (TransactionUpdate == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                var Transaction = await transactionRepository.GetByIdAsync(id);
                if (Transaction == null)
                    return NotFound("Транзакции с таким id не существует");

                Transaction.StatusId = TransactionUpdate.StatusId; // у транзакции из внешних ключей может меняться толкьо статус
                Transaction.TransactionDate = TransactionUpdate.TransactionDate;

                await transactionRepository.UpdateAsync(Transaction);
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
                var Transaction = await transactionRepository.GetByIdAsync(id);
                if (Transaction == null)
                    return NotFound("Транзакции с таким id не существует");
                await transactionRepository.DeleteAsync(Transaction);
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
