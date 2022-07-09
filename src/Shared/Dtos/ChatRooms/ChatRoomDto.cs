using StockChatroom.Shared.Dtos.Messages;

namespace StockChatroom.Shared.Dtos.ChatRooms;

public class ChatRoomDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<MessageDto> Messages { get; set; }
}
