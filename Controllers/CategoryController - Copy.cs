using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data.Context;
using OrderManagementSystem.Data.Entity;

namespace OrderManagementSystem.Api.Controllers
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public static void SeedData(ApplicationDbContext context)
        {
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product { Name = "Test Product 1", Price = 10.99m },
                    new Product { Name = "Test Product 2", Price = 25.50m },
                    new Product { Name = "Test Product 3", Price = 15.75m }
                );
                context.SaveChanges();
            }
        }
    }

}
