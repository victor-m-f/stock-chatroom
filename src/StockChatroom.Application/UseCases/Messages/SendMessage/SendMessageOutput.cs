using System.Net;

namespace StockChatroom.Application.UseCases.Messages.SendMessage;

public class SendMessageOutput : OutputBase
{
    public SendMessageOutput(HttpStatusCode httpStatusCode)
        : base(httpStatusCode)
    {
    }

    public SendMessageOutput(bool isValid, HttpStatusCode httpStatusCode)
        : base(isValid, httpStatusCode)
    {
    }
}
