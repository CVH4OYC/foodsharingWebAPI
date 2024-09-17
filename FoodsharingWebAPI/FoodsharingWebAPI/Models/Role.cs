using System.ComponentModel.DataAnnotations;

namespace FoodsharingWebAPI.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}
