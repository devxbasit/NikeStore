using Microsoft.EntityFrameworkCore;
using NikeStore.Services.RewardsApi.Models;

namespace NikeStore.Services.CouponApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Rewards> Rewards { get; set; }
}