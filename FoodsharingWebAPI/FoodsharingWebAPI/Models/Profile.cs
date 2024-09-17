using System.ComponentModel.DataAnnotations;

namespace FoodsharingWebAPI.Models

{
    public class Profile
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "Длина имени превышает 50 символов!")]
        public string FirstName { get; set; }
        [StringLength(50, ErrorMessage = "Длина фамилии превышает 50 символов!")]
        public string? LastName { get; set; }
        public string? Image { get; set; }
        [StringLength(500, ErrorMessage = "Длина описания превышает 500 символов!")]
        public string? Bio {  get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
