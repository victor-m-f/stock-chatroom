using MediatR;

namespace StockChatroom.Application.UseCases.ChatRooms.GetChatRoomDetail;

public class GetChatRoomDetailInput : IRequest<GetChatRoomDetailOutput>
{
    public Guid ChatRoomId { get; set; }

    public GetChatRoomDetailInput(Guid chatRoomId)
    {
        ChatRoomId = chatRoomId;
    }
}
