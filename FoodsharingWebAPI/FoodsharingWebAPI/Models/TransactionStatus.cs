using System.ComponentModel.DataAnnotations;

namespace FoodsharingWebAPI.Models
{
    /// <summary>
    /// Статус транзакции
    /// </summary>
    public class TransactionStatus
    {
        /// <summary>
        /// Id статуса транзакции
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Название статуса транзакции
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Навигационное свойство для связи с таблицей Transaction
        /// </summary>
        public List<Transaction>? Transactions { get; set; }
    }
}
