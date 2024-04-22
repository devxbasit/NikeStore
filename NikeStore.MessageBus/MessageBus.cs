using System.Text;
using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using NikeStore.MessageBus.Contracts;
using NikeStore.MessageBus.Model;

namespace NikeStore.MessageBus;

public class MessageBus : IMessageBus
{
    private readonly MessageBusSettings _messageBusSettings;

    public MessageBus(MessageBusSettings messageBusSettings)
    {
        _messageBusSettings = messageBusSettings;
    }

    public async Task PublishMessage(object message, string queueOrTopicName)
    {
        try
        {
            await using var client = new ServiceBusClient(_messageBusSettings.ConnectionString);

            ServiceBusSender sender = client.CreateSender(queueOrTopicName);

            string jsonMessage = JsonConvert.SerializeObject(message);
            byte[] bytes = Encoding.UTF8.GetBytes(jsonMessage);

            ServiceBusMessage serviceBusMessage = new ServiceBusMessage(bytes)
            {
                CorrelationId = Guid.NewGuid().ToString()
            };

            await sender.SendMessageAsync(serviceBusMessage);
            await client.DisposeAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}