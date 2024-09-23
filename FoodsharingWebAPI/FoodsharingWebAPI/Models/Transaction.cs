using System.ComponentModel.DataAnnotations;

namespace FoodsharingWebAPI.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int SenderId { get; set; }
        public User? Sender { get; set; }
        [Required]
        public int RecipientId { get; set; }
        public User? Recipient { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }
        [Required]
        public int StatusId { get; set; }
        public TransactionStatus? Status { get; set; }
        [Required]
        public int AnnouncementId { get; set; }
        public Announcement? Announcement { get; set; }
    }
}
