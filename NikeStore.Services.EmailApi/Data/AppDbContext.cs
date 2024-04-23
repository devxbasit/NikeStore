using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using NikeStore.Services.EmailApi.Models;

namespace NikeStore.Services.EmailApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<EmailLogger> EmailLogger { get; set; }
    
}