using System.Runtime.InteropServices.JavaScript;
using System.Text;
using Hangfire;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NikeStore.Services.EmailApi.Message;
using NikeStore.Services.EmailApi.Models;
using NikeStore.Services.EmailApi.Services.IService;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NikeStore.Services.EmailApi.RabbitMqConsumer;

public class RabbitMqEmailCartConsumer : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly IDbLogService _dbLogService;
    private readonly ISmtpMailService _smtpMailService;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queueName;


    public RabbitMqEmailCartConsumer(IConfiguration configuration, IDbLogService dbLogService,
        IOptionsMonitor<RabbitMQConnectionOptions> rabbitMqConnectionOptions, ISmtpMailService smtpMailService)
    {
        _configuration = configuration;
        _dbLogService = dbLogService;
        _smtpMailService = smtpMailService;

        _queueName = _configuration.GetValue<string>("RabbitMQSetting:QueueNames:EmailShoppingCartQueue");

        var connectionFactory = new ConnectionFactory()
        {
            HostName = rabbitMqConnectionOptions.CurrentValue.HostName,
            UserName = rabbitMqConnectionOptions.CurrentValue.UserName,
            Password = rabbitMqConnectionOptions.CurrentValue.Password,
            VirtualHost = rabbitMqConnectionOptions.CurrentValue.VirtualHost
        };

        _connection = connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(_queueName, true, false, false, null);
    }


    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = GetEmailCartQueueConsumer();
            _channel.BasicConsume(_queueName, false, consumer);

            return Task.CompletedTask;
        }
        catch (Exception e)
        {
            _connection.Close();

            Console.WriteLine(e);
            throw;
        }
    }

    private EventingBasicConsumer GetEmailCartQueueConsumer()
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (channel, eventArgs) =>
        {
            var content = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            EmailCartMessage message = JsonConvert.DeserializeObject<EmailCartMessage>(content);

            var dbLogMessage = GenerateDbLogForEmailCart(message);

            BackgroundJob.Enqueue(() => _dbLogService.LogToDb(dbLogMessage));
            BackgroundJob.Enqueue(() => _smtpMailService.SendMail(dbLogMessage));
            _channel.BasicAck(eventArgs.DeliveryTag, false);
        };

        return consumer;
    }


    private DbMailLogs GenerateDbLogForEmailCart(EmailCartMessage message)
    {
        var log = new DbMailLogs()
        {
            To = message.CartHeader.Email,
            Subject = "Your Shopping Cart from NikeStore!",
            Body = GetMailBody(message),
            CreatedDateTime = DateTime.Now,
            IsProcessed = false
        };

        return log;
    }

    private string GetMailBody(EmailCartMessage emailCartMessage)
    {
        var webPanelAddress = _configuration.GetValue<string>($"ServiceUrls:NikeStoreWebPanelUrl") ?? throw new ArgumentNullException();
        var cartUrl = $"{webPanelAddress}/Cart/CartIndex";
        var topLogoUrl = $"{webPanelAddress}/img/nikestore-heart-logo.png";

        var cartTotal = 0;

        StringBuilder productListStringBuilder = new StringBuilder();

        foreach (var item in emailCartMessage.CartDetails)
        {
            productListStringBuilder.Append(@$"
                                       <tr>
                                          <td width='50%' style='padding-right: 10px;'>
                                             <img src='{item.Product.ImageUrl}' alt='Product Image' style='max-width: 100%; height: 150px;'>
                                          </td>
                                          <td width='50%' style='padding-left: 10px;'>
                                             <h3 style='color: #333333; font-size: 25px; margin-top: 0;'>{item.Product.Name}</h3>
                                             <p style='color: #666666; font-size: 17px; margin-bottom: 0px;'>Price: {item.Product.Price}</p>
                                             <p style='color: #666666; font-size: 17px; margin-bottom: 0px;'>Quantity: {item.Count}</p>
                                          </td>
                                       </tr>");

            cartTotal += (int)item.Product.Price * item.Product.Count;
        }

        return $@"
                <!DOCTYPE html>
                <html lang='en' xmlns='http://www.w3.org/1999/xhtml' xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office'>
                   <head>
                      <meta charset='UTF-8'>
                      <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                      <meta http-equiv='X-UA-Compatible' content='ie=edge'>
                      <title>Cart Email</title>
                      <style>
                         /* Add your custom styles here */
                      </style>
                   </head>
                   <body style='margin: 0; padding: 0; background-color: #f4f4f4;'>
                      <table cellpadding='0' cellspacing='0' width='100%' style='border-collapse: collapse;'>
                         <tr>
                            <td align='center' style='padding: 20px 0;'>
                               <table cellpadding='0' cellspacing='0' width='600' style='border-collapse: collapse; border: none;'>
                                  <tr>
                                     <td align='center' bgcolor='#ffffff' style='padding: 40px; border-radius: 5px 5px 0 0;'>
                                        <img src='{topLogoUrl}' alt='Logo' style='display: block; max-width: 500px;'>
                                     </td>
                                  </tr>
                                  <tr>
                                     <td bgcolor='#ffffff' style='padding: 40px;'>
                                        <h2 style='color: #333333; font-size: 24px; margin-top: 0;'>Hey {emailCartMessage.CartHeader.Name}, your cart is waiting!</h2>
                                        <p style='color: #666666; font-size: 16px; line-height: 1.5;'>You left some goodies in your cart at NikeStore. Don't worry, they're saved for you!</p>
                                        <p style='color: #666666; font-size: 16px; line-height: 1.5;'>Take a look at your cart items again:</p>
                                        <table cellpadding='0' cellspacing='0' width='100%' style='border-collapse: collapse;'>

                                           {productListStringBuilder.ToString()}

                                        </table>
                                     </td>
                                  </tr>
                                  <tr>
                                     <td bgcolor='#ffffff' style='padding: 20px 40px; padding-top:0px'>
                                        <h3 style='color: #333333; font-size: 25px;'>Cart Total = {emailCartMessage.CartHeader.CartTotal}</h3>
                                        <hr/>
                                        <p>Ready to complete your purchase? Click the button below to head straight to your cart.</p>
                                        <div class='cta'>
                                        <a href='{cartUrl}' style='display: inline-block; padding: 10px 20px; background-color: #007bff; color: #ffffff; text-decoration: none; border-radius: 5px;'>Complete Purchase</a>
                                     </td>
                                  </tr>
                                  <tr>
                                     <td bgcolor='#ffffff' style='padding: 20px 40px;'>
                                        <p style='color: #666666; font-size: 14px; margin-bottom: 0;'>&copy; {DateTime.Now.ToString("yyyy")} NikeStore. All rights reserved. If you have any questions or need assistance, feel free to contact us at 1800-5555-7777.</p>
                                     </td>
                                  </tr>
                               </table>
                            </td>
                         </tr>
                      </table>
                   </body>
                </html>";
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
        return base.StopAsync(cancellationToken);
    }
}
