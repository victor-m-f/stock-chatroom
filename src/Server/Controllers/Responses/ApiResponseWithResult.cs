using Newtonsoft.Json;

namespace StockChatroom.Server.Controllers.Responses;

public class ApiResponseWithResult<T> : ApiResponse
{
    [JsonProperty("result")]
    public T? Result { get; set; }

    public ApiResponseWithResult(int httpStatusCode)
        : base(httpStatusCode)
    {
    }

    public ApiResponseWithResult(T result, int httpStatusCode)
        : base(httpStatusCode)
    {
        Result = result;
    }

    public ApiResponseWithResult(IEnumerable<ApiError> errors, int httpStatusCode)
        : base(httpStatusCode, errors)
    {
    }
}
