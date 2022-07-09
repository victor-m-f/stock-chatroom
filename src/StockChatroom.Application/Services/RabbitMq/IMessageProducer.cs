namespace StockChatroom.Application.Services.RabbitMq;

public interface IMessageProducer
{
    public void SendMessage<T>(T message);
}
