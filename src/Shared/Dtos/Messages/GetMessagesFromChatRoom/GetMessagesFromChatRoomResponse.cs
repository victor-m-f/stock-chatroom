namespace StockChatroom.Shared.Dtos.Messages.GetMessagesFromChatRoom;

public class GetMessagesFromChatRoomResponse
{
    public IEnumerable<MessageDto> MessageDto { get; set; }
}
