using System.ComponentModel.DataAnnotations;

namespace FoodsharingWebAPI.Models
{
    /// <summary>
    /// Организация
    /// </summary>
    public class Organization
    {
        /// <summary>
        /// Id организации
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Название организации
        /// </summary>
        [StringLength(50, ErrorMessage = "Длина имени превышает 50 символов!")]
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Внешний ключ, указывающий на адрес организации
        /// </summary>
        [Required]
        public int AddressId { get; set; }
        /// <summary>
        /// Навигационное свойство для связи с таблицей Address
        /// </summary>
        public Address? Address { get; set; }
        /// <summary>
        /// Номер телефона организации
        /// </summary>
        [Required]
        public string Phone {  get; set; }
        /// <summary>
        /// Адрес электронной почты организации
        /// </summary>
        [RegularExpression(@"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$", ErrorMessage = "Неверный формат Email!")]
        [Required]
        public string Email { get; set; }
        /// <summary>
        /// Ссылка на сайт организации
        /// </summary>
        public string? Website { get; set; }

        /// <summary>
        /// Описание организации
        /// </summary>
        [StringLength(500, ErrorMessage = "Длина описания превышает 50 символов!")]
        public string? Description { get; set; }
        /// <summary>
        /// Навигационное свойство для связи с таблицей Representative
        /// </summary>
        public List<Representative>? Representatives { get; set; }
    }
}
