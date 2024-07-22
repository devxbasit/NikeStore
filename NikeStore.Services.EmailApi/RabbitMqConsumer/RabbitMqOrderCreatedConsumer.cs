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

public class RabbitMqOrderCreatedConsumer : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly IDbLogService _dbLogService;
    private readonly ISmtpMailService _smtpMailService;
    private  RabbitMQConnectionOptions _rabbitMqConnectionOptions;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    private readonly string _exchangeName;
    private readonly string _queueName;
    private readonly string _routingKey;

    public RabbitMqOrderCreatedConsumer(IConfiguration configuration, IDbLogService dbLogService, IOptionsMonitor<RabbitMQConnectionOptions> rabbitMqConnectionOptions, ISmtpMailService smtpMailService)
    {
        _configuration = configuration;
        _dbLogService = dbLogService;
        _smtpMailService = smtpMailService;
        _rabbitMqConnectionOptions = rabbitMqConnectionOptions.CurrentValue;
        rabbitMqConnectionOptions.OnChange((newOptionsValue) => _rabbitMqConnectionOptions = newOptionsValue);


        var connectionFactory = new ConnectionFactory()
        {
            HostName = _rabbitMqConnectionOptions.HostName,
            UserName = _rabbitMqConnectionOptions.UserName,
            Password = _rabbitMqConnectionOptions.Password,
            VirtualHost = _rabbitMqConnectionOptions.VirtualHost
        };

        _connection = connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();

        _exchangeName = _configuration.GetValue<string>("RabbitMQSetting:ExchangeNames:OrdersExchange");
        _routingKey = _configuration.GetValue<string>("RabbitMQSetting:RoutingKeys:NewOrderRoutingKey");
        _queueName = _configuration.GetValue<string>("RabbitMQSetting:QueueNames:OrderCreatedQueue");

        _channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct, durable: false);
        _channel.QueueDeclare(_queueName, true, false, false, null);
        _channel.QueueBind(_queueName, _exchangeName, _routingKey);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = GetOrderCreatedQueueConsumer();

            _channel.BasicConsume(_queueName, false, consumer);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private EventingBasicConsumer GetOrderCreatedQueueConsumer()
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (ch, eventArgs) =>
        {
            var content = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            OrderCreatedMessage orderCreatedMessage = JsonConvert.DeserializeObject<OrderCreatedMessage>(content);

            var dbLogMessage = GenerateDbLogForOrderCreated(orderCreatedMessage);
            BackgroundJob.Enqueue(() => _dbLogService.LogToDb(dbLogMessage));
            BackgroundJob.Enqueue(() => _smtpMailService.SendMail(dbLogMessage));
        };

        return consumer;
    }


    private DbMailLogs GenerateDbLogForOrderCreated(OrderCreatedMessage orderCreatedMessage)
    {
        var log = new DbMailLogs()
        {
            To = orderCreatedMessage.Email,
            Subject = "Your NikeStore order is confirmed!",
            Body = GetMailBody(orderCreatedMessage),
            CreatedDateTime = DateTime.Now,
            IsProcessed = false
        };

        return log;
    }

    private string GetMailBody(OrderCreatedMessage orderCreatedMessage)
    {
        return $@"
            <!DOCTYPE html>
            <html lang='en'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Order Confirmation</title>
            </head>
            <body>
                <!-- Header -->
                <table width='100%' cellpadding='0' cellspacing='0' border='0'>
                    <tr>
                        <td align='center'>
                            <table width='600' cellpadding='0' cellspacing='0' border='0'>
                                <tr>
                                    <td class='header' style='background-color: #00BFFF; padding: 40px; text-align: center; color: white; font-size: 24px;'>
                                    Hooray! Your order is confirmed.
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

                <!-- Body -->
                <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                    <tr>
                        <td align='center' style='padding: 20px;'>
                            <table class='content' width='600' border='0' cellspacing='0' cellpadding='0' style='border-collapse: collapse; border: 1px solid #cccccc;'>
                                <tr>
                                    <td class='body' style='padding: 40px; text-align: left; font-size: 16px; line-height: 1.6;'>
                                        Dear {orderCreatedMessage.Name},<br>
                                        Thank you for placing an order with us! Your order has been successfully created.<br><br>
                                        Order Details:<br>
                                        Order Number: #{orderCreatedMessage.OrderHeaderId}<br>
                                        Date: {orderCreatedMessage.OrderCreatedDateTime.ToString("dddd, dd MMMM yyyy HH:mm")}<br>
                                        Total Amount: {orderCreatedMessage.OrderTotal}<br><br>
                                        We will process your order and notify you once it's shipped. If you have any questions, feel free to contact our customer support.Thank you for choosing us.
                                        <br>
                                        <br>
                                        <b>Happing Shopping!</b><br />
                                        <b>The NikeStore Team</b>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </body>
            </html>
            ";
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
        return base.StopAsync(cancellationToken);
    }
}
