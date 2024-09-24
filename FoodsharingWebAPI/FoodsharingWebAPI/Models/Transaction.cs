using System.ComponentModel.DataAnnotations;

namespace FoodsharingWebAPI.Models
{
    /// <summary>
    /// Транзакция
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Id транзакции
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Внешний ключ, указывающий на пользователя, отдающего продукты
        /// </summary>
        [Required]
        public int SenderId { get; set; }
        /// <summary>
        /// Навигационное свойство для связи с пользователем, отдающим продукты, из таблицы User
        /// </summary>
        public User? Sender { get; set; }
        /// <summary>
        /// Внешний ключ, указывающий на пользователя, получающего продукты
        /// </summary>
        [Required]
        public int RecipientId { get; set; }
        /// <summary>
        /// Навигационное свойство для связи с получателем продуктов из таблицы User
        /// </summary>
        public User? Recipient { get; set; }
        /// <summary>
        /// Дата последней смены статуса транзакции
        /// </summary>
        [Required]
        public DateTime TransactionDate { get; set; }
        /// <summary>
        /// Внешний ключ, указывающий на статус транзакции
        /// </summary>
        [Required]
        public int StatusId { get; set; }
        /// <summary>
        /// Навигационное свойство для связи с таблицей TransactionStatus
        /// </summary>
        public TransactionStatus? Status { get; set; }
        /// <summary>
        /// Внешний ключ, указывающий на объявление, с которым связана транзакция
        /// </summary>
        [Required]
        public int AnnouncementId { get; set; }
        /// <summary>
        /// Навигационное свойство для связи с таблицей Announcement
        /// </summary>
        public Announcement? Announcement { get; set; }
    }
}
