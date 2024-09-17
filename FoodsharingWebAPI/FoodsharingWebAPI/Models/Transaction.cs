using System.ComponentModel.DataAnnotations;

namespace FoodsharingWebAPI.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public int SenderId { get; set; }
        public User Sender { get; set; }
        public int RecipientId { get; set; }
        public User Recipient { get; set; }
        public DateTime TransactionDate { get; set; }
        public int StatusId { get; set; }
        public TransactionStatus Status { get; set; }
        public int AnnouncementId { get; set; }
        public Announcement Announcement { get; set; }
    }
}
