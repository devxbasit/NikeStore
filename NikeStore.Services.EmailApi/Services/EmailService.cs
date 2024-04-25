using System.Text;
using Microsoft.EntityFrameworkCore;
using NikeStore.Services.EmailApi.Data;
using NikeStore.Services.EmailApi.Message;
using NikeStore.Services.EmailApi.Models;
using NikeStore.Services.EmailApi.Models.Dto;
using NikeStore.Services.EmailApi.Services.IService;

namespace NikeStore.Services.EmailApi.Services;

public class EmailService : IEmailService
{
    private DbContextOptions<AppDbContext> _dbContextOptions;

    public EmailService(DbContextOptions<AppDbContext> dbContextOptions)
    {
        _dbContextOptions = _dbContextOptions;
    }

    public async Task EmailCartAndLog(CartDto cartDto)
    {
        StringBuilder message = new StringBuilder();

        message.AppendLine("<br /> Cart Email Requested");
        message.AppendLine($"<br /> Total: {cartDto.CartHeader.CartTotal}");
        message.Append("<br />");

        message.Append("<ul>");

        foreach (var item in cartDto.CartDetails)
        {
            message.Append("<li>");

            message.Append($"{item.Product.Name} x {item.Count}");

            message.Append("<li />");
        }

        message.Append("<ul />");

        await LogAndEmail(message.ToString(), "basitshafi.dev@gmail.com");
    }

    public async Task RegisterUserEmailAndLog(string email)
    {
        string message = "User Registeration Successful. <br/> Email : " + email;
        await LogAndEmail(message, "dotnetmastery@gmail.com");
    }

    public async Task LogOrderPlaced(RewardsMessage rewardsMessage)
    {
        string message = "New Order Placed. <br/> Order ID : " + rewardsMessage.OrderId;
        await LogAndEmail(message, "basitshafi.dev@gmail.com");
    }

    private async Task<bool> LogAndEmail(string message, string email)
    {
        try
        {
            await using var db = new AppDbContext(_dbContextOptions);

            EmailLogger emailLog = new()
            {
                Email = email,
                Message = message,
                EmailSentDateTime = DateTime.UtcNow
            };

            await db.EmailLoggers.AddAsync(emailLog);
            await db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}