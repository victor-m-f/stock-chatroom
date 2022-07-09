namespace StockChatroom.Client.Services.Implementations;

internal class StockChatRoomService : IStockChatRoomService
{
    public IUsersService Users { get; }
    public IMessagesService Messages { get; }
    public IChatRoomsService ChatRooms { get; }

    public StockChatRoomService(IUsersService users, IMessagesService messages, IChatRoomsService chatRooms)
    {
        Users = users;
        Messages = messages;
        ChatRooms = chatRooms;
    }
}
