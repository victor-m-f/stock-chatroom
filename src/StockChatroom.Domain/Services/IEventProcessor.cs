namespace StockChatroom.Domain.Services;

public interface IEventProcessor
{
    public Task ProcessEvent(string eventMessage, CancellationToken cancellationToken);
}
