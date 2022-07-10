using StockChatroom.Application.Services.Hubs;
using System.Net;

namespace StockChatroom.Application.UseCases.Messages.SendMessage;

public class NotifyMessageUseCase : INotifyMessageUseCase
{
    private readonly SignalRHub _signalRHub;

    public NotifyMessageUseCase(SignalRHub signalRHub)
    {
        _signalRHub = signalRHub;
    }

    public async Task<NotifyMessageOutput> Handle(NotifyMessageInput request, CancellationToken cancellationToken)
    {
        await _signalRHub.SendMessageAsync(request.Message, request.ChatRoomId, cancellationToken);
        await _signalRHub.SendChatNotificationAsync(
            request.MessageNotification,
            request.ChatRoomId,
            request.Message.FromUser.Id,
            cancellationToken);

        return new NotifyMessageOutput(HttpStatusCode.OK);
    }
}
