using System.ComponentModel.DataAnnotations;

namespace SBIShopify.Models.DTO
{
    public class LoginDTO
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
