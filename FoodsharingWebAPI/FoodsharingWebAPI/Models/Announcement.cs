using System.ComponentModel.DataAnnotations;

namespace FoodsharingWebAPI.Models
{
    public class Announcement
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "Длина заголовка превышает 50 символов!")]
        public string Title { get; set; }
        [StringLength(500, ErrorMessage = "Длина описания превышает 500 символов!")]
        public string? Description { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Image {  get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
