using System.Text;
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
    private readonly IConnection _connection;
    private IModel _channel;

    private string _exchangeName = "";
    private string _queueName = "";

    public RabbitMqOrderCreatedConsumer(IConfiguration configuration, IDbLogService dbLogService, IOptions<RabbitMQConnectionOptions> rabbitMqConnectionOptions)
    {
        _configuration = configuration;
        _dbLogService = dbLogService;

        _exchangeName = _configuration.GetValue<string>("RabbitMQSetting:ExchangeNames:OrderCreatedExchange");
        _queueName = _configuration.GetValue<string>("RabbitMQSetting:QueueNames:OrderCreatedQueue");

        var connectionFactory = new ConnectionFactory()
        {
            HostName = rabbitMqConnectionOptions.Value.HostName,
            UserName = rabbitMqConnectionOptions.Value.UserName,
            Password = rabbitMqConnectionOptions.Value.Password
        };

        _connection = connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct);
        _channel.QueueDeclare(_queueName, false, false, false, null);

        _channel.QueueBind(_queueName, _exchangeName, _configuration.GetValue<string>("RabbitMQSetting:RoutingKeys:EmailUpdateRoutingKey"));
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
            HandleMessage(orderCreatedMessage).GetAwaiter().GetResult();
        };

        return consumer;
    }

    private async Task HandleMessage(OrderCreatedMessage orderCreatedMessage)
    {
        //await _dbLogService.LogOrderPlaced(orderCreatedMessage);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
        return base.StopAsync(cancellationToken);
    }
}
