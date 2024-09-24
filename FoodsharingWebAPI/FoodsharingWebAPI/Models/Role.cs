using System.ComponentModel.DataAnnotations;

namespace FoodsharingWebAPI.Models
{
    /// <summary>
    /// Роль
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Id роли
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Название роли
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Навигационное свойство для связи с таблицей UserRole
        /// </summary>
        public List<UserRole>? UserRoles { get; set; }
    }
}
