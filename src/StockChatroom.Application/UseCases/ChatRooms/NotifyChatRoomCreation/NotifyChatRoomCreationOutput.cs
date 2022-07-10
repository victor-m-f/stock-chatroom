using System.Net;

namespace StockChatroom.Application.UseCases.ChatRooms.CreateChatRoom;

public class NotifyChatRoomCreationOutput : OutputBase
{
    public NotifyChatRoomCreationOutput(HttpStatusCode httpStatusCode)
        : base(httpStatusCode)
    {
    }

    public NotifyChatRoomCreationOutput(bool isValid, HttpStatusCode httpStatusCode)
        : base(isValid, httpStatusCode)
    {
    }
}
