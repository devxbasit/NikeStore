using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using Microsoft.Extensions.Options;
using NikeStore.Services.OrderApi.Models;

namespace NikeStore.Services.OrderAPI.RabbmitMQSender
{
    public class RabbitMqOrderMessageProducer : IRabbitMqOrderMessageProducer
    {
        private IConnection _connection;
        private readonly IConfiguration _configuration;
        private readonly RabbitMQConnectionOptions _rabbitMqConnectionOptions;

        public RabbitMqOrderMessageProducer(IConfiguration configuration, IOptions<RabbitMQConnectionOptions> rabbitMqConnectionOptions)
        {
            _configuration = configuration;
            _rabbitMqConnectionOptions = rabbitMqConnectionOptions.Value;
        }

        public void SendMessage(object message, string exchangeName)
        {
            if (!ConnectionExists()) CreateConnection();

            var emailUpdateQueueName = _configuration.GetValue<string>("RabbitMQSetting:QueueNames:EmailUpdatedQueue");
            var rewardUpdateQueueName = _configuration.GetValue<string>("RabbitMQSetting:QueueNames:RewardsUpdateQueue");
            var emailUpdateRoutingKey = _configuration.GetValue<string>("RabbitMQSetting:RoutingKeys:EmailUpdateRoutingKey");
            var rewardUpdateRoutingKey = _configuration.GetValue<string>("RabbitMQSetting:RoutingKeys:RewardsUpdateRoutingKey");

            using var channel = _connection.CreateModel();

            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, durable: false);
            channel.QueueDeclare(emailUpdateQueueName, false, false, false, null);
            channel.QueueDeclare(rewardUpdateQueueName, false, false, false, null);


            channel.QueueBind(emailUpdateQueueName, exchangeName, emailUpdateRoutingKey);
            channel.QueueBind(rewardUpdateQueueName, exchangeName, rewardUpdateRoutingKey);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            //channel.BasicPublish(exchange: exchangeName, emailUpdateRoutingKey, null, body: body);
            channel.BasicPublish(exchange: exchangeName, rewardUpdateRoutingKey, null, body: body);
        }

        private bool ConnectionExists()
        {
            if (_connection is null) return false;

            return true;
        }

        private void CreateConnection()
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = _rabbitMqConnectionOptions.HostName,
                UserName = _rabbitMqConnectionOptions.UserName,
                Password = _rabbitMqConnectionOptions.Password
            };

            _connection = connectionFactory.CreateConnection();
        }
    }
}
