using NikeStore.Services.EmailApi.Messaging;

namespace NikeStore.Services.EmailApi.Extensions;

public static class ServiceExtensions
{

    public static void AddRabbitMqConsumers(this IServiceCollection services)
    {
        services.AddHostedService<RabbitMQAuthConsumer>();
        services.AddHostedService<RabbitMQCartConsumer>();
        services.AddHostedService<RabbitMQOrderConsumer>();
    }
    
    
}