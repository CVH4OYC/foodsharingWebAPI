using System.ComponentModel.DataAnnotations;

namespace FoodsharingWebAPI.Models
{
    public class Representative
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int OrganizationId { get; set; }
        public Organization? Organization { get; set; }
    }
}
