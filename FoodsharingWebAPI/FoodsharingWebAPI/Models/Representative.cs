using System.ComponentModel.DataAnnotations;

namespace FoodsharingWebAPI.Models
{
    /// <summary>
    /// Представитель организации
    /// </summary>
    public class Representative
    {
        /// <summary>
        /// Id представителя организации
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Внешний ключ, указывающий на пользователя, который является представителем организации
        /// </summary>
        [Required]
        public int UserId { get; set; }
        /// <summary>
        /// Навигационное свойство для связи с таблицей User
        /// </summary>
        public User? User { get; set; }
        /// <summary>
        /// Внешний ключ, указывающий на организацию, в которой работает представитель
        /// </summary>
        [Required]
        public int OrganizationId { get; set; }
        /// <summary>
        /// Навигационное свойство для связи с таблицей Organization
        /// </summary>
        public Organization? Organization { get; set; }
    }
}
