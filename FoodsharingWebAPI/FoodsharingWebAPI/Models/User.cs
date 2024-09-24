using System.ComponentModel.DataAnnotations;
using System.Runtime;

namespace FoodsharingWebAPI.Models
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        [StringLength(30, ErrorMessage = "Длина имени превышает 30 символов!")]
        [Required]
        public string UserName { get; set; }
        /// <summary>
        /// Хэш пароля пользователя
        /// </summary>
        [Required]
        public string Password { get; set; }
        /// <summary>
        /// Навигационное свойство для связи с таблицей Profile
        /// </summary>
        public Profile? Profile { get; set; }
        /// <summary>
        /// Навигационное свойство для связи с таблицей UserRole
        /// </summary>
        public List<UserRole>? UserRoles { get; set; }
        /// <summary>
        /// Навигационное свойство для связи с таблицей Representative
        /// </summary>
        public Representative? Representative { get; set; }
        /// <summary>
        /// Навигационное свойство для связи с таблицей Announcement
        /// </summary>
        public List<Announcement>? Announcements { get; set; }
        /// <summary>
        /// Навигационное свойство для связи с таблицей Transaction
        /// </summary>
        public List<Transaction>? Transactions { get; set; }
        /// <summary>
        /// Навигационное свойство для связи с таблицей Chat
        /// </summary>
        public List<Chat>? Chats { get; set; }
        /// <summary>
        /// Навигационное свойство для связи с таблицей Message
        /// </summary>
        public List<Message>? Messages { get; set; }
    }
}
