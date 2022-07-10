using StockChatroom.Application.Services.Hubs;
using StockChatroom.Shared.Dtos.ChatRooms;
using System.Net;

namespace StockChatroom.Application.UseCases.ChatRooms.CreateChatRoom;

public class NotifyChatRoomCreationUseCase : INotifyChatRoomCreationUseCase
{
    private readonly SignalRHub _signalRHub;

    public NotifyChatRoomCreationUseCase(SignalRHub signalRHub)
    {
        _signalRHub = signalRHub;
    }

    public async Task<NotifyChatRoomCreationOutput> Handle(NotifyChatRoomCreationInput request, CancellationToken cancellationToken)
    {
        await _signalRHub.CreateChatRoomAsync(new ChatRoomDto() { Id = request.ChatRoom.Id, Name = request.ChatRoom.Name }, cancellationToken);

        return new NotifyChatRoomCreationOutput(HttpStatusCode.Created);
    }
}
