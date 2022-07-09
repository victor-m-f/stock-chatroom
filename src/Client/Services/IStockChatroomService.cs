namespace StockChatroom.Client.Services;

public interface IStockChatRoomService
{
    public IUsersService Users { get; }
    public IMessagesService Messages { get; }
    public IChatRoomsService ChatRooms { get; }
}
