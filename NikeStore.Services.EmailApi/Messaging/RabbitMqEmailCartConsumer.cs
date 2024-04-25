using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NikeStore.Services.EmailApi.Models;
using NikeStore.Services.EmailApi.Models.Dto;
using NikeStore.Services.EmailApi.Services.IService;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NikeStore.Services.EmailApi.Messaging;

public class RabbitMqEmailCartConsumer : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queueName;


    public RabbitMqEmailCartConsumer(IConfiguration configuration, IEmailService emailService,
        IOptions<RabbitMQConnectionOptions> rabbitMqConnectionOptions)
    {
        _configuration = configuration;
        _emailService = emailService;

        _queueName = _configuration.GetValue<string>("RabbitMQSetting:QueueNames:EmailShoppingCartQueue");

        var connectionFactory = new ConnectionFactory()
        {
            HostName = rabbitMqConnectionOptions.Value.HostName,
            UserName = rabbitMqConnectionOptions.Value.UserName,
            Password = rabbitMqConnectionOptions.Value.Password
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
            CartDto cartDto = JsonConvert.DeserializeObject<CartDto>(content);
            HandleMessage(cartDto).GetAwaiter().GetResult();
            _channel.BasicAck(eventArgs.DeliveryTag, false);
        };

        return consumer;
    }


    private async Task HandleMessage(CartDto cartDto)
    {
        Console.WriteLine("Handling message...");
        await _emailService.EmailCartAndLog(cartDto);
    }
}