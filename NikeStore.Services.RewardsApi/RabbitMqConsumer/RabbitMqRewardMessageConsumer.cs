using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NikeStore.Services.OrderApi.Models;
using NikeStore.Services.RewardsApi.Message;
using NikeStore.Services.RewardsApi.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NikeStore.Services.RewardsApi.RabbitMqConsumer;

public class RabbitMqRewardMessageConsumer : BackgroundService
{
    private readonly IRewardService _rewardService;
    private IModel _channel;
    private string _queueName = "";

    public RabbitMqRewardMessageConsumer(IConfiguration configuration, IRewardService rewardService, IOptions<RabbitMQConnectionOptions> rabbitMqConnectionOptions)
    {
        _rewardService = rewardService;

        var exchangeName = configuration.GetValue<string>("RabbitMQSetting:ExchangeNames:OrderCreatedExchange");
        var routingKey = configuration.GetValue<string>("RabbitMQSetting:RoutingKeys:RewardsUpdateRoutingKey");
        _queueName = configuration.GetValue<string>("RabbitMQSetting:QueueNames:RewardsUpdateQueue");


        var connectionFactory = new ConnectionFactory()
        {
            HostName = rabbitMqConnectionOptions.Value.HostName,
            UserName = rabbitMqConnectionOptions.Value.UserName,
            Password = rabbitMqConnectionOptions.Value.Password
        };


        var connection = connectionFactory.CreateConnection();

        _channel = connection.CreateModel();
        _channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
        _channel.QueueDeclare(_queueName, false, false, false, null);
        _channel.QueueBind(_queueName, exchangeName, routingKey);
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
            try
            {
                var content = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                RewardsMessage rewardsMessage = JsonConvert.DeserializeObject<RewardsMessage>(content);
                HandleMessage(rewardsMessage).GetAwaiter().GetResult();

                _channel.BasicAck(eventArgs.DeliveryTag, false);
            }
            catch (Exception e)
            {
                _channel.BasicReject(eventArgs.DeliveryTag, false);
            }
        };

        return consumer;
    }

    private async Task HandleMessage(RewardsMessage rewardsMessage)
    {
        await _rewardService.UpdateRewards(rewardsMessage);
    }
}
