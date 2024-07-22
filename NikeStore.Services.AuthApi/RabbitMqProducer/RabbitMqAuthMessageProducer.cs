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
    private RabbitMQConnectionOptions _rabbitMqConnectionOptions;

    public RabbitMqAuthMessageProducer(IConfiguration configuration,
        IOptionsMonitor<RabbitMQConnectionOptions> rabbitMqConnectionOptions)
    {
        _configuration = configuration;
        _rabbitMqConnectionOptions = rabbitMqConnectionOptions.CurrentValue;
        rabbitMqConnectionOptions.OnChange((newOptionsValue) => _rabbitMqConnectionOptions = newOptionsValue);
    }

    public void SendMessage(object message, string queueName)
    {
        string jsonMessage = JsonConvert.SerializeObject(message);
        byte[] body = Encoding.UTF8.GetBytes(jsonMessage);

        if (!ConnectionExists()) CreateConnection();
        var channel = _connection.CreateModel();
        channel.QueueDeclare(queueName, true, false, false, null);
        channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
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
            UserName = _rabbitMqConnectionOptions.UserName,
            Password = _rabbitMqConnectionOptions.Password,
            HostName = _rabbitMqConnectionOptions.HostName,
            VirtualHost = _rabbitMqConnectionOptions.VirtualHost
        };

        _connection = connectionFactory.CreateConnection();
    }
}
