using System.ComponentModel.DataAnnotations;

namespace SBIShopify.Models.DTO
{
    public class AddToCartDTO
    {
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string CustomerUserId { get; set; }
        [Required]
        public Guid ProductId { get; set; } 

    }
}
