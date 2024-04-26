using Microsoft.EntityFrameworkCore;
using NikeStore.Services.ProductApi.Models;

namespace NikeStore.Services.ProductApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Product> Products { get; set; }
}