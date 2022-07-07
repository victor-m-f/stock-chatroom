using MediatR;

namespace StockChatroom.Application.UseCases.ChatRooms.CreateChatRoom;

public interface ICreateChatRoomUseCase : IRequestHandler<CreateChatRoomInput, CreateChatRoomOutput>
{
}
