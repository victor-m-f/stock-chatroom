using MediatR;

namespace StockChatroom.Application.UseCases.Messages.GetMessagesFromChatRoom;

public interface IGetMessagesFromChatRoomUseCase : IRequestHandler<GetMessagesFromChatRoomInput, GetMessagesFromChatRoomOutput>
{
}
