using StockChatroom.Shared.Dtos.ChatRooms;

namespace StockChatroom.Shared.Events;

public class ChatRoomCreatedEvent : EventBase
{
    public ChatRoomDto ChatRoom { get; set; }
}
