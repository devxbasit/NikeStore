namespace Mango.Services.OrderAPI.RabbmitMQSender
{
    public interface IRabbitMqOrderMessageProducer
    {
        void SendMessage(Object message, string exchangeName);
    }
}
