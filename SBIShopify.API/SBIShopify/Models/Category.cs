using System.ComponentModel.DataAnnotations;

namespace SBIShopify.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
