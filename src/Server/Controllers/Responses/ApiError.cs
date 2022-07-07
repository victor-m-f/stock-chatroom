namespace StockChatroom.Server.Controllers.Responses;

public class ApiError
{
    public string Message { get; }

    public ApiError(string message)
    {
        Message = message;
    }
}
