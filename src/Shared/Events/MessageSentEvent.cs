using StockChatroom.Shared.Dtos.Messages;

namespace StockChatroom.Shared.Events;

public class MessageSentEvent : EventBase
{
    public MessageDto Message { get; set; }
    public string MessageNotification { get; set; }
    public Guid ChatRoomId { get; set; }
}
