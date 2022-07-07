using MediatR;

namespace StockChatroom.Application.UseCases.Messages.GetMessagesFromChatRoom;

public class GetMessagesFromChatRoomInput : IRequest<GetMessagesFromChatRoomOutput>
{
    public Guid ChatRoomId { get; set; }

    public GetMessagesFromChatRoomInput(Guid chatRoomId)
    {
        ChatRoomId = chatRoomId;
    }
}
