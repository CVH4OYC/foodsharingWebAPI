using System.ComponentModel.DataAnnotations;

namespace FoodsharingWebAPI.Models
{
    public class MessageStatus
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Message>? Messages { get; set; }
    }
}
