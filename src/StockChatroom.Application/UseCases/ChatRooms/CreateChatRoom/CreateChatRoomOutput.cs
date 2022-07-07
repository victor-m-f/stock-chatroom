using System.Net;

namespace StockChatroom.Application.UseCases.ChatRooms.CreateChatRoom;

public class CreateChatRoomOutput : OutputBase
{
    public CreateChatRoomOutput(HttpStatusCode httpStatusCode)
        : base(httpStatusCode)
    {
    }

    public CreateChatRoomOutput(bool isValid, HttpStatusCode httpStatusCode)
        : base(isValid, httpStatusCode)
    {
    }
}
