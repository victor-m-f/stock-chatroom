﻿using Microsoft.AspNetCore.SignalR;
using StockChatroom.Shared.Dtos.Messages;

namespace StockChatroom.Server.Hubs;

public class SignalRHub : Hub
{
    public async Task SendMessageAsync(MessageDto message, string userName) =>
        await Clients.All.SendAsync("ReceiveMessage", message, userName);

    public async Task ChatNotificationAsync(string message, string receiverUserId, string senderUserId) =>
        await Clients.All.SendAsync("ReceiveChatNotification", message, receiverUserId, senderUserId);
}
