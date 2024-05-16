namespace NikeStore.Services.OrderAPI.RabbmitMQSender
{
    public interface IRabbitMqOrderMessageProducer
    {
        void SendMessage(object message, string routingKey);
    }
}
