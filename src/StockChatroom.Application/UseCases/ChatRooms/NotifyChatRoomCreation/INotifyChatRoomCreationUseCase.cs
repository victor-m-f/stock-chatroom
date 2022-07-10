using MediatR;

namespace StockChatroom.Application.UseCases.ChatRooms.CreateChatRoom;

public interface INotifyChatRoomCreationUseCase : IRequestHandler<NotifyChatRoomCreationInput, NotifyChatRoomCreationOutput>
{
}
