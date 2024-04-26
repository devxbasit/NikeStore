using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NikeStore.Services.EmailApi.Models;
using RabbitMQ.Client;

namespace NikeStore.Services.AuthApi.RabbitMqProducer;

public class RabbitMqAuthMessageProducer : IRabbitMqAuthMessageProducer
{
    private readonly IConfiguration _configuration;
    private IConnection _connection;
    private readonly IModel _channel;
    private string _queueName;


    public RabbitMqAuthMessageProducer(IConfiguration configuration,
        IOptions<RabbitMQConnectionOptions> rabbitMqConnectionOptions)
    {
        _configuration = configuration;

        _queueName = _configuration.GetValue<string>("RabbitMQSetting:QueueNames:RegisterUserQueue");

        var connectionFactory = new ConnectionFactory()
        {
            HostName = rabbitMqConnectionOptions.Value.HostName,
            UserName = rabbitMqConnectionOptions.Value.UserName,
            Password = rabbitMqConnectionOptions.Value.Password
        };

        _connection = connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(_queueName, true, false, false);
    }


    public void SendMessage(object message)
    {
        string jsonMessage = JsonConvert.SerializeObject(message);
        byte[] body = Encoding.UTF8.GetBytes(jsonMessage);


        _channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
    }
}