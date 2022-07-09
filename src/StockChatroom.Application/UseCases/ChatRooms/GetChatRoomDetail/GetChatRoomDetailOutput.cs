using StockChatroom.Domain.Entities;
using System.Net;

namespace StockChatroom.Application.UseCases.ChatRooms.GetChatRoomDetail;

public class GetChatRoomDetailOutput : OutputBase
{
    public ChatRoom? ChatRoom { get; }

    public GetChatRoomDetailOutput(ChatRoom chatRoom, HttpStatusCode httpStatusCode)
        : base(httpStatusCode)
    {
        ChatRoom = chatRoom;
    }

    public GetChatRoomDetailOutput(bool isValid, HttpStatusCode httpStatusCode)
        : base(isValid, httpStatusCode)
    {
    }
}
