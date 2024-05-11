using System.Text;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NikeStore.Services.EmailApi.Models;
using NikeStore.Services.EmailApi.Services.IService;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NikeStore.Services.EmailApi.RabbitMqConsumer;

public class RabbitMqAuthConsumer : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;

    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queueName;

    public RabbitMqAuthConsumer(IConfiguration configuration, IEmailService emailService,
        IOptions<RabbitMQConnectionOptions> rabbitMqConnectionOptions)
    {
        _configuration = configuration;
        _emailService = emailService;

        var connectionFactory = new ConnectionFactory()
        {
            HostName = rabbitMqConnectionOptions.Value.HostName,
            UserName = rabbitMqConnectionOptions.Value.UserName,
            Password = rabbitMqConnectionOptions.Value.Password
        };

        _connection = connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();

        _queueName = _configuration.GetValue<string>("RabbitMQSetting:QueueNames:UserRegisteredQueue");
        _channel.QueueDeclare(_queueName, true, false, false);
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
                var message = JsonConvert.DeserializeObject<string>(jsonContent);
                HandleMessage(message);
                _channel.BasicAck(eventArgs.DeliveryTag, false);
            }
            catch (Exception e)
            {
                _channel.BasicReject(eventArgs.DeliveryTag, false);
            }
        };

        return consumer;
    }

    private void HandleMessage(string email)
    {
        _emailService.RegisterUserEmailAndLog(email);
    }
}