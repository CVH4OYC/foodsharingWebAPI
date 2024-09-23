using System.ComponentModel.DataAnnotations;

namespace FoodsharingWebAPI.Models
{
    public class Chat
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int FirstUserId { get; set; }
        public User? FirstUser { get; set; }
        [Required]
        public int SecondUserId { get; set; }
        public User? SecondUser { get; set; }
        public List<Message>? Messages { get; set; }
    }
}
