using StockChatroom.Shared.Dtos.Messages;

namespace StockChatroom.Shared.Events;

public class CommandSentEvent : EventBase
{
    public MessageDto Message { get; set; }
    public Guid ChatRoomId { get; set; }
}
