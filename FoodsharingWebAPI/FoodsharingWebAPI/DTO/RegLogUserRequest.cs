using System.ComponentModel.DataAnnotations;

namespace FoodsharingWebAPI.DTO
{
    public class RegLogUserRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
