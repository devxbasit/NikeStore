using System.Diagnostics.Tracing;
using System.Text;
using Newtonsoft.Json;
using NikeStore.Services.EmailApi.Message;
using NikeStore.Services.EmailApi.Services.IService;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NikeStore.Services.EmailApi.Messaging;

public class RabbitMQOrderConsumer : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;
    private readonly IConnection _connection;
    private IModel _channel;

    private string _exchangeName = "";
    private string _queueName = "";
    private const string _orderCreated_EmailUpdateQueue = "EmailUpdateQueue";

    public RabbitMQOrderConsumer(IConfiguration configuration, IEmailService emailService)
    {
        _configuration = configuration;
        _emailService = emailService;
        _exchangeName = _configuration.GetValue<string>("TopicAndQueueNames:OrderCreatedTopic");

        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            Password = "guest",
            UserName = "guest",
        };


        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct);
        _channel.QueueDeclare(_orderCreated_EmailUpdateQueue, false, false, false, null);
        _channel.QueueBind(_orderCreated_EmailUpdateQueue, _exchangeName, "EmailUpdate");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = GetOrderQueueConsumer();
            _channel.BasicConsume(_queueName, false, consumer);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private EventingBasicConsumer GetOrderQueueConsumer()
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