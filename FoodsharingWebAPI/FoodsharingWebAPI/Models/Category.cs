using System.ComponentModel.DataAnnotations;

namespace FoodsharingWebAPI.Models
{
    /// <summary>
    /// Категория продуктов питания
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Id категории продуктов питания
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Название категории продуктов питания
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Навигационное свойство для связи с таблицей Announcement
        /// </summary>
        public List<Announcement>? Announcements { get; set; } 
    }
}
