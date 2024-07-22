using Microsoft.EntityFrameworkCore;
using NikeStore.Services.EmailApi.Data;
using NikeStore.Services.EmailApi.Models;
using NikeStore.Services.EmailApi.Services.IService;

namespace NikeStore.Services.EmailApi.Services;

public class DbLogService : IDbLogService
{
    private DbContextOptions<AppDbContext> _dbContextOptions;

    public DbLogService(DbContextOptions<AppDbContext> dbContextOptions)
    {
        _dbContextOptions = dbContextOptions;
    }

    public async Task LogToDb(DbMailLogs log)
    {
        await using var db = new AppDbContext(_dbContextOptions);
        await db.DbMailLogs.AddAsync(log);
        await db.SaveChangesAsync();
    }
}
