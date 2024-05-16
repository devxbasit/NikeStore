using System.Net.Mail;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using NikeStore.Services.EmailApi.Models;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace NikeStore.Services.EmailApi.Services.IService;

public class SmtpMailService : ISmtpMailService
{
    private readonly SmtpClient _smtpClient;
    private readonly MailKitConnectionOptions _mailKitConnectionOptions;

    public SmtpMailService(IOptions<MailKitConnectionOptions> mailKitConnectionOptions)
    {
        _mailKitConnectionOptions = mailKitConnectionOptions.Value;
        // using var smtp = new SmtpClient();
        _smtpClient = new SmtpClient();
        _smtpClient.Connect(_mailKitConnectionOptions.SenderMailAddressHost, _mailKitConnectionOptions.SenderMailAddressHostPort, SecureSocketOptions.StartTls);
        _smtpClient.Authenticate(_mailKitConnectionOptions.SenderMailAddress, _mailKitConnectionOptions.SenderMailAddressPassword);
    }

    public async Task<bool> SendMail(DbMailLogs message)
    {
        try
        {
            var email = new MimeMessage()
            {
                From = { MailboxAddress.Parse(_mailKitConnectionOptions.SenderMailAddress) },
                To = { MailboxAddress.Parse(message.To) },
                Subject = message.Subject,
                Body = new TextPart(TextFormat.Html)
                {
                    Text = message.Body
                },
            };

            var response = await _smtpClient.SendAsync(email);


        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _smtpClient.Disconnect(true);
            throw;
        }

        return false;
    }
}
