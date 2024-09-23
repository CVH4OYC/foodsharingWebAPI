using System.ComponentModel.DataAnnotations;
using System.Runtime;

namespace FoodsharingWebAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [StringLength(30, ErrorMessage = "Длина имени превышает 30 символов!")]
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public Profile? Profile { get; set; }
        public List<UserRole>? UserRoles { get; set; }
        public Representative? Representative { get; set; }
        public List<Announcement>? Announcements { get; set; }
        public List<Transaction>? Transactions { get; set; }
        public List<Chat>? Chats { get; set; }
        public List<Message>? Messages { get; set; }
    }
}
