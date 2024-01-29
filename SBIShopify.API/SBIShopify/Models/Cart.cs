using System.ComponentModel.DataAnnotations;

namespace SBIShopify.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public Guid CustomerId { get; set; }
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
       
    }
}
