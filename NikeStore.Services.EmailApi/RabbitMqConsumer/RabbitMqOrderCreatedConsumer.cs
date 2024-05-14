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
    private readonly IEmailService _emailService;
    private readonly IConnection _connection;
    private IModel _channel;

    private string _exchangeName = "";
    private string _queueName = "";
    
    public RabbitMqOrderCreatedConsumer(IConfiguration configuration, IEmailService emailService,  IOptions<RabbitMQConnectionOptions> rabbitMqConnectionOptions)
    {
        _configuration = configuration;
        _emailService = emailService;
        
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
            RewardsMessage rewardsMessage = JsonConvert.DeserializeObject<RewardsMessage>(content);
            HandleMessage(rewardsMessage).GetAwaiter().GetResult();
        };

        return consumer;
    }

    private async Task HandleMessage(RewardsMessage rewardsMessage)
    {
        await _emailService.LogOrderPlaced(rewardsMessage);
    }
}