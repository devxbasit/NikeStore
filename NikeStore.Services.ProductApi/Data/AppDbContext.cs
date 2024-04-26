using Microsoft.EntityFrameworkCore;
using NikeStore.Services.ProductApi.Models;

namespace NikeStore.Services.ProductApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Product> Products { get; set; }
    
    
     protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "Test Product",
                Price = 15,
                Description = "Test Description",
                ImageUrl = "https://placehold.co/603x403",
                CategoryName = "Appetizer"
            });
           
        }
}