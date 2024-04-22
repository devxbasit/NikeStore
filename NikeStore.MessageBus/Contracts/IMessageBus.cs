namespace NikeStore.MessageBus.Contracts;

public interface IMessageBus
{
    Task PublishMessage(object message, string queueOrTopicName);
}