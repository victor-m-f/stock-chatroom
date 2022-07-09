using System.Net;

namespace StockChatroom.Application.UseCases.Messages.ProccessCommand;

public class ProccessCommandOutput : OutputBase
{
    public ProccessCommandOutput(HttpStatusCode httpStatusCode)
        : base(httpStatusCode)
    {
    }

    public ProccessCommandOutput(bool isValid, HttpStatusCode httpStatusCode)
        : base(isValid, httpStatusCode)
    {
    }
}
