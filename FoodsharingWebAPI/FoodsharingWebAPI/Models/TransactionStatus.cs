using System.ComponentModel.DataAnnotations;

namespace FoodsharingWebAPI.Models
{
    public class TransactionStatus
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
