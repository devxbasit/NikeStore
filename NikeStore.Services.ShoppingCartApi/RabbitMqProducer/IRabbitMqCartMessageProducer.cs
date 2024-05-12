namespace NikeStore.Services.ShoppingCartApi.RabbitMqProducer;

public interface IRabbitMqCartMessageProducer
{
    void SendMessage(object message, string queueName);
}