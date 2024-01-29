using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace SBIShopify.Models
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        public long Contact { get; set; }
        public string EmailId { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

    }
}
