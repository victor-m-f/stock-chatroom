using Microsoft.AspNetCore.SignalR;
using StockChatroom.Shared.Dtos.ChatRooms;
using StockChatroom.Shared.Dtos.Messages;

namespace StockChatroom.Application.Services.Hubs;

public class SignalRHub : Hub
{
    private readonly IHubContext<SignalRHub> _context;

    public SignalRHub(IHubContext<SignalRHub> context)
    {
        _context = context;
    }

    public async Task SendMessageAsync(MessageDto message, Guid chatRoomId, CancellationToken cancellationToken) =>
        await _context.Clients.All.SendAsync("ReceiveMessage", message, chatRoomId, cancellationToken);

    public async Task SendChatNotificationAsync(string message, Guid toChatRoomId, string senderUserId, CancellationToken cancellationToken) =>
        await _context.Clients.All.SendAsync("ReceiveChatNotification", message, toChatRoomId, senderUserId, cancellationToken);

    public async Task CreateChatRoomAsync(ChatRoomDto chatRoom, CancellationToken cancellationToken) =>
        await _context.Clients.All.SendAsync("CreateChatRoom", chatRoom, cancellationToken);
}
