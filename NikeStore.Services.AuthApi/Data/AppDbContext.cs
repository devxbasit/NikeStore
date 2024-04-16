using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NikeStore.Services.AuthApi.Models;

namespace NikeStore.Services.AuthApi.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // will not create any table with name "ApplicationUsers", will just add extra columns to AspNetUsers table
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}