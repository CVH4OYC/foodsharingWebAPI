using System.ComponentModel.DataAnnotations;

namespace FoodsharingWebAPI.Models
{
    public class Organization
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "Длина имени превышает 50 символов!")]
        [Required]
        public string Name { get; set; }
        [Required]
        public int AddressId { get; set; }
        public Address? Address { get; set; }

        [Required]
        public string Phone {  get; set; }
        [RegularExpression(@"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$", ErrorMessage = "Неверный формат Email!")]
        [Required]
        public string Email { get; set; }
        public string? Website { get; set; }
        [StringLength(500, ErrorMessage = "Длина описания превышает 50 символов!")]
        public string? Description { get; set; }
        public List<Representative>? Representatives { get; set; }
    }
}
