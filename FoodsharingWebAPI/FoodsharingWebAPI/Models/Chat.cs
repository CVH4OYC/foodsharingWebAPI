using System.ComponentModel.DataAnnotations;

namespace FoodsharingWebAPI.Models
{
    /// <summary>
    /// Чат
    /// </summary>
    public class Chat
    {
        /// <summary>
        /// Id чата
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Внешний ключ, указывающий на первого собеседника в чате
        /// </summary>
        [Required]
        public int FirstUserId { get; set; }
        /// <summary>
        /// Навигационное свойство для связи с первым пользователем из таблицы User
        /// </summary>
        public User? FirstUser { get; set; }
        /// <summary>
        /// Внешний ключ, указывающий на второго собеседника в чате
        /// </summary>
        [Required]
        public int SecondUserId { get; set; }
        /// <summary>
        /// Навигационное свойство для связи со вторым пользователем из таблицы User
        /// </summary>
        public User? SecondUser { get; set; }
        /// <summary>
        /// Навигационное свойство для связи с таблицей Message
        /// </summary>
        public List<Message>? Messages { get; set; }
    }
}
