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

    // public async Task EmailCartAndLog(CartDto cartDto)
    // {
    //     StringBuilder message = new StringBuilder();
    //
    //     message.AppendLine("<br /> Cart Email Requested");
    //     message.AppendLine($"<br /> Total: {cartDto.CartHeader.CartTotal}");
    //     message.Append("<br />");
    //
    //     message.Append("<ul>");
    //
    //     foreach (var item in cartDto.CartDetails)
    //     {
    //         message.Append("<li>");
    //
    //         message.Append($"{item.Product.Name} x {item.Count}");
    //
    //         message.Append("<li />");
    //     }
    //
    //     message.Append("<ul />");
    //
    //     await LogAndEmail(message.ToString(), "basitshafi.dev@gmail.com");
    // }
    //
    // public async Task RegisterUserEmailAndLog(string email)
    // {
    //     string message = "User Registeration Successful. <br/> Email : " + email;
    //     await LogAndEmail(message, "basitshafi.dev@gmail.com");
    // }
    //
    // public async Task LogOrderPlaced(OrderCreatedMessage orderCreatedMessage)
    // {
    //     string message = "New Order Placed. <br/> Order ID : " + orderCreatedMessage.OrderHeaderId;
    //     await LogAndEmail(message, "basitshafi.dev@gmail.com");
    // }
    //
    // private async Task<bool> LogAndEmail(string message, string email)
    // {
    //     try
    //     {
    //
    //     }
    //     catch (Exception ex)
    //     {
    //         return false;
    //     }
    // }


}
