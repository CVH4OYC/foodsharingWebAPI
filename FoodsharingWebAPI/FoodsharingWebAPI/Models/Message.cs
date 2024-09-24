using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks.Dataflow;

namespace FoodsharingWebAPI.Models
{
    /// <summary>
    /// Сообщение
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Id сообщения
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Внешний ключ, указывающий на пользователя, который отправил сообщения
        /// </summary>
        [Required]
        public int SenderId { get; set; }
        /// <summary>
        /// Навигационное свойство для связи с таблицей User
        /// </summary>
        public User? Sender { get; set; }
        /// <summary>
        /// Внешний ключ, указывающий на чат, в котором отправлено сообщение
        /// </summary>
        [Required]
        public int ChatId { get; set; }
        /// <summary>
        /// Навигационное свойство для связи с таблицей Chat
        /// </summary>
        public Chat? Chat { get; set; }
        /// <summary>
        /// Текст сообщения
        /// </summary>

        [StringLength(500, ErrorMessage ="Длина сообщения превышает 500 символов!")]
        [Required]
        public string Text { get; set; }
        /// <summary>
        /// Дата отправки сообщения
        /// </summary>
        [Required]
        public DateTime Date { get; set; }
        /// <summary>
        /// Внешний ключ, указывающий на статус сообщения
        /// </summary>
        [Required]
        public int StatusId { get; set; }
        /// <summary>
        /// Навигационное свойство для связи с таблицей MessageStatus
        /// </summary>
        public MessageStatus? Status { get; set; }
        /// <summary>
        /// Путь до изображения, прикреплённого к сообщению
        /// </summary>
        public string? Image {  get; set; }
        /// <summary>
        /// Путь до файла, прикреплённого к сообщению
        /// </summary>
        public string? File { get; set; }
    }
}
