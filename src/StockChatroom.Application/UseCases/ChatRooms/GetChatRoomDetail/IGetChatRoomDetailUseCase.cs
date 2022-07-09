using MediatR;

namespace StockChatroom.Application.UseCases.ChatRooms.GetChatRoomDetail;

public interface IGetChatRoomDetailUseCase : IRequestHandler<GetChatRoomDetailInput, GetChatRoomDetailOutput>
{
}
