using System.ComponentModel.DataAnnotations;

namespace FoodsharingWebAPI.Models
{
    /// <summary>
    /// Объявление
    /// </summary>
    public class Announcement
    {
        /// <summary>
        /// Id объявления
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Заголовок объявления
        /// </summary>

        [Required]
        [StringLength(50, ErrorMessage = "Длина заголовка превышает 50 символов!")]
        public string Title { get; set; }
        /// <summary>
        /// Описание объявления
        /// </summary>

        [StringLength(500, ErrorMessage = "Длина описания превышает 500 символов!")]
        public string? Description { get; set; }
        /// <summary>
        /// Внешний ключ, указывающий на адрес
        /// </summary>

        [Required]
        public int AddressId { get; set; }
        /// <summary>
        /// Навигационное свойство для связи с таблицей Address
        /// </summary>
        public Address Address { get; set; }
        /// <summary>
        /// Дата создания объявления
        /// </summary>

        [Required]
        public DateTime DateCreation { get; set; }
        /// <summary>
        /// Срок годности продукта из объявления
        /// </summary>

        [Required]
        public DateTime ExpirationDate { get; set; }
        /// <summary>
        /// Путь к картинке
        /// </summary>

        [Required]
        public string Image {  get; set; }
        /// <summary>
        /// Внешний ключ, указывающий на категорию продукта питания, указанного в объявлении
        /// </summary>

        [Required]
        public int CategoryId { get; set; }
        /// <summary>
        /// Навигационное свойство для связи с таблицей Category
        /// </summary>
        public Category? Category { get; set; }
        /// <summary>
        /// Внешний ключ, указывающий на пользователя, который создал объявление
        /// </summary>
        [Required]
        public int UserId { get; set; }
        /// <summary>
        /// Навигационное свойство для связи с таблицей User
        /// </summary>
        public User? User { get; set; }
        /// <summary>
        /// Навигационное свойство для связи с таблицей Transaction
        /// </summary>
        public List<Transaction>? Transactions { get; set; }
    }
}
