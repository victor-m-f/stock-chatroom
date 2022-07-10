using System.Net;

namespace StockChatroom.Application.UseCases.Messages.SendMessage;

public class NotifyMessageOutput : OutputBase
{
    public NotifyMessageOutput(HttpStatusCode httpStatusCode)
        : base(httpStatusCode)
    {
    }

    public NotifyMessageOutput(bool isValid, HttpStatusCode httpStatusCode)
        : base(isValid, httpStatusCode)
    {
    }
}
