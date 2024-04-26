namespace NikeStore.Services.AuthApi.RabbitMqProducer;

public interface IRabbitMqAuthMessageProducer
{
    void SendMessage(object message);
}