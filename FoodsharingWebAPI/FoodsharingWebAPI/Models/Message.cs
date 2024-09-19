using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks.Dataflow;

namespace FoodsharingWebAPI.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public int SenderId { get; set; }
        public User? Sender { get; set; }
        public int ChatId { get; set; }
        public Chat? Chat { get; set; }
        [StringLength(500, ErrorMessage ="Длина сообщения превышает 500 символов!")]
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int StatusId { get; set; }
        public MessageStatus? Status { get; set; }
        public string? Image {  get; set; }
        public string? File { get; set; }
    }
}
