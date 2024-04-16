using Microsoft.EntityFrameworkCore;
using NikeStore.Services.CouponApi.Models;

namespace NikeStore.Services.CouponApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Coupon> Coupons { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Coupon>().HasData(
            new Coupon()
            {
                CouponId = 1,
                CouponCode = "GUEST_100_OFF",
                DiscountAmount = 100,
                MinAmount = 1500
            }
        );

        modelBuilder.Entity<Coupon>().HasData(
            new Coupon()
            {
                CouponId = 2,
                CouponCode = "GUEST_200_OFF",
                DiscountAmount = 200,
                MinAmount = 2500
            }
        );
    }
}