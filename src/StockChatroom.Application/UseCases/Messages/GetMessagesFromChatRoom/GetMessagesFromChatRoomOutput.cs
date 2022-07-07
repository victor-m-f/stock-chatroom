using StockChatroom.Domain.Entities;
using System.Net;

namespace StockChatroom.Application.UseCases.Messages.GetMessagesFromChatRoom;

public class GetMessagesFromChatRoomOutput : OutputBase
{
    public IEnumerable<Message> Messages { get; set; }

    public GetMessagesFromChatRoomOutput(IEnumerable<Message> messages, HttpStatusCode httpStatusCode)
        : base(httpStatusCode)
    {
        Messages = messages;
    }

    public GetMessagesFromChatRoomOutput(bool isValid, HttpStatusCode httpStatusCode)
        : base(isValid, httpStatusCode)
    {
    }
}
