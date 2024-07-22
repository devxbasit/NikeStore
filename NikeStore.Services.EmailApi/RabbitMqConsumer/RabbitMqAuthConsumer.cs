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

public class RabbitMqAuthConsumer : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly IDbLogService _dbLogService;
    private readonly ISmtpMailService _smtpMailService;

    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queueName;

    public RabbitMqAuthConsumer(IConfiguration configuration, IDbLogService dbLogService,IOptionsMonitor<RabbitMQConnectionOptions> rabbitMqConnectionOptions, ISmtpMailService smtpMailService)
    {
        _configuration = configuration;
        _dbLogService = dbLogService;
        _smtpMailService = smtpMailService;

        var connectionFactory = new ConnectionFactory()
        {
            HostName = rabbitMqConnectionOptions.CurrentValue.HostName,
            UserName = rabbitMqConnectionOptions.CurrentValue.UserName,
            Password = rabbitMqConnectionOptions.CurrentValue.Password,
            VirtualHost = rabbitMqConnectionOptions.CurrentValue.VirtualHost
        };

        _connection = connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();

        _queueName = _configuration.GetValue<string>("RabbitMQSetting:QueueNames:UserRegisteredQueue");
        _channel.QueueDeclare(_queueName, true, false, false, null);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
        return base.StopAsync(cancellationToken);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = GetAuthQueueConsumer();
            _channel.BasicConsume(_queueName, false, consumer);

            return Task.CompletedTask;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private EventingBasicConsumer GetAuthQueueConsumer()
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (channel, eventArgs) =>
        {
            try
            {
                var jsonContent = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                var message = JsonConvert.DeserializeObject<UserRegisteredMessage>(jsonContent);

                var dbLogMessage = GenerateDbLogForUserRegistration(message);

                BackgroundJob.Enqueue(() => _dbLogService.LogToDb(dbLogMessage));
                BackgroundJob.Enqueue(() => _smtpMailService.SendMail(dbLogMessage));

                _channel.BasicAck(eventArgs.DeliveryTag, false);
            }
            catch (Exception e)
            {
                _channel.BasicReject(eventArgs.DeliveryTag, false);
            }
        };

        return consumer;
    }

    private DbMailLogs GenerateDbLogForUserRegistration(UserRegisteredMessage message)
    {
        var log = new DbMailLogs()
        {
             To = message.Email,
            Subject = "Thanks for registration with NikeStore!",
            Body = GetMailBody(message),
            CreatedDateTime = DateTime.Now,
            IsProcessed = false
        };

        return log;
    }

    private string GetMailBody(UserRegisteredMessage message)
    {
        var webPanelAddress = _configuration.GetValue<string>($"ServiceUrls:NikeStoreWebPanelUrl") ?? throw new ArgumentNullException();
        var loginUrl = $"{webPanelAddress}/Auth/Login";

        return $@"
            <!DOCTYPE html>
            <html lang='en'>
            <head>
                <meta charset='UTF-8'>
                <meta http-equiv='X-UA-Compatible' content='IE=edge'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>User Registration Confirmation</title>
            </head>
            <body style='font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px;'>
                <table style='max-width: 600px; margin: 0 auto; background: #fff; padding: 20px; border-radius: 5px;'>
                    <tr>
                        <td style='text-align: center;'>
                            <h2>Welcome to <span style='color:red'>NikeStore!</span></h2>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>Dear <b>{message.Name}</b>,</p>
                            <p>Thank you for registering with us. Your account has been successfully created.</p>
                            <p>You can now access all our features and start exploring our platform.</p>
                        </td>
                    </tr>
                    <tr>
                        <td style='text-align: center;'>
                            <a href='{loginUrl}' style='display: inline-block; padding: 10px 20px; background-color: #007bff; color: #fff; text-decoration: none; border-radius: 5px;'>Login Now</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>If you have any questions or need assistance, feel free to contact our support team.</p>
                            <p>Thank you for joining us!</p>
                        </td>
                    </tr>
                </table>
            </body>
            </html>";
    }
}
