using StockChatroom.Domain.Entities;
using System.Net;

namespace StockChatroom.Application.UseCases.ChatRooms.GetAllChatRooms;

public class GetAllChatRoomsOutput : OutputBase
{
    public List<ChatRoom>? ChatRooms { get; }

    public GetAllChatRoomsOutput(List<ChatRoom> chatRooms, HttpStatusCode httpStatusCode)
        : base(httpStatusCode)
    {
        ChatRooms = chatRooms;
    }

    public GetAllChatRoomsOutput(bool isValid, HttpStatusCode httpStatusCode)
        : base(isValid, httpStatusCode)
    {
    }
}
