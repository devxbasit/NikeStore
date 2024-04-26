using Microsoft.EntityFrameworkCore;
using NikeStore.Services.ShoppingCartApi.Models;

namespace NikeStore.Services.ShoppingCartApi.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<CartHeader> CartHeaders { get; set; }
    public DbSet<CartDetails> CartDetails { get; set; }
     
}