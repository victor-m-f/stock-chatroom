using StockChatroom.Client.Services.Implementations;

namespace StockChatroom.Client.Services.Configuration;

public static class StockChatRoomConfiguration
{
    public static void ConfigureStockChatRoom(this IServiceCollection services)
    {
        _ = services.AddTransient<IUsersService, UsersService>();
        _ = services.AddTransient<IMessagesService, MessagesService>();
        _ = services.AddTransient<IChatRoomsService, ChatRoomsService>();
        _ = services.AddTransient<IStockChatRoomService, StockChatRoomService>();
    }
}
