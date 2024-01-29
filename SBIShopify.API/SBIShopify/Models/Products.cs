﻿using System.ComponentModel.DataAnnotations;

namespace SBIShopify.Models
{
    public class Products
    {
        [Key]
        public Guid Id { get; set; }    
        public string Name { get; set; }
        public string Price { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Categories { get; set; }



    }
}
