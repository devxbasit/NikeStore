using NikeStore.Services.EmailApi.RabbitMqConsumer;

namespace NikeStore.Services.EmailApi.Extensions;

public static class ServiceExtensions
{

    public static void AddRabbitMqConsumersAsHostedService(this IServiceCollection services)
    {
        services.AddHostedService<RabbitMqAuthConsumer>();
        services.AddHostedService<RabbitMqEmailCartConsumer>();
        services.AddHostedService<RabbitMqOrderCreatedConsumer>();
    }
}
