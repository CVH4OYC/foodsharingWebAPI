using System.ComponentModel.DataAnnotations;

namespace FoodsharingWebAPI.Models
{
    /// <summary>
    /// Роли пользователей
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// Id сущности о роли пользователя
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Внешний ключ, указывающий на роль
        /// </summary>
        [Required]
        public int RoleId { get; set; }
        /// <summary>
        /// Внешний ключ, указывающий на пользователя
        /// </summary>
        [Required]
        public int UserId { get; set; }
        /// <summary>
        /// Навигационное свойство для связи с таблицей Role
        /// </summary>
        public Role Role { get; set; }
        /// <summary>
        /// Навигационное свойство для связи с таблицей User
        /// </summary>
        public User User { get; set; }
    }
}
