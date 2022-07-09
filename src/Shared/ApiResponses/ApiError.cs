namespace StockChatroom.Shared.ApiResponses;

public class ApiError
{
    public string Message { get; }

    public ApiError(string message)
    {
        Message = message;
    }
}
