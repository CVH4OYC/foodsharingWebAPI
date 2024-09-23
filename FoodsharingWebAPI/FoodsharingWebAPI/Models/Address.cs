using System.ComponentModel.DataAnnotations;

namespace FoodsharingWebAPI.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Длина названия региона превышает 100 символов!")]
        public string Region { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Длина названия города превышает 100 символов!")]
        public string City { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Длина названия улицы превышает 100 символов!")]
        public string Street { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Длина номера дома превышает 10 символов!")]
        public string House { get; set; }
        public Organization? Organization { get; set; }
        public List<Announcement>? Announcements { get; set; }
    }
}
