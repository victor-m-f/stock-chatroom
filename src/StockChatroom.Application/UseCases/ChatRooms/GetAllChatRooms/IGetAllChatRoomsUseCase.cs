using MediatR;

namespace StockChatroom.Application.UseCases.ChatRooms.GetAllChatRooms;

public interface IGetAllChatRoomsUseCase : IRequestHandler<GetAllChatRoomsInput, GetAllChatRoomsOutput>
{
}
