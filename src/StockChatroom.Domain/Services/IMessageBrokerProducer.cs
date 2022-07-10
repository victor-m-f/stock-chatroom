namespace StockChatroom.Domain.Services;

public interface IMessageBrokerProducer
{
    public void PublishEvent(object @event);
}
