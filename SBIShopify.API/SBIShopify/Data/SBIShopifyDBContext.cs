using Microsoft.EntityFrameworkCore;
using SBIShopify.Models;

namespace SBIShopify.Data
{
    public class SBIShopifyDBContext : DbContext
    {
        public SBIShopifyDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Cart>()
            //    .HasOne(c => c.Customers)
            //    .WithOne(cu => cu.Cart)
            //    .HasForeignKey<Cart>(c => c.CustomerId);
            //modelBuilder.Entity<Cart>()
            //   .HasOne(c => c.Products)
            //   .WithOne(p => p.Cart)
            //   .HasForeignKey<Cart>(c => c.ProductId);
        }

    }

}

