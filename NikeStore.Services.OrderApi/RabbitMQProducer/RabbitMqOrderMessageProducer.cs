using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using Microsoft.Extensions.Options;
using NikeStore.Services.OrderApi.Models;

namespace NikeStore.Services.OrderAPI.RabbmitMQSender
{
    public class RabbitMqOrderMessageProducer : IRabbitMqOrderMessageProducer
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IConfiguration _configuration;
        private  RabbitMQConnectionOptions _rabbitMqConnectionOptions;

        private readonly string _exchangeName;
        private readonly string _queueName;
        private readonly string _routingKey;

        public RabbitMqOrderMessageProducer(IConfiguration configuration, IOptionsMonitor<RabbitMQConnectionOptions> rabbitMqConnectionOptions)
        {
            _configuration = configuration;
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

        public void SendMessage(object message, string routingKey)
        {
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            _channel.BasicPublish(_exchangeName, _routingKey, null, body: body);
        }
    }
}
