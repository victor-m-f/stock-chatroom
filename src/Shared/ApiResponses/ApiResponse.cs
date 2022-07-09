using Newtonsoft.Json;
using System.Text.Json;

namespace StockChatroom.Shared.ApiResponses;

public class ApiResponse
{
    #region Properties

    [JsonProperty("succeeded")]
    public bool Succeeded { get; set; }
    [JsonProperty("errors")]
    public IEnumerable<ApiError> Errors { get; set; } = Enumerable.Empty<ApiError>();
    [JsonProperty("httpStatusCode")]
    public int HttpStatusCode { get; set; }

    #endregion

    #region Constructors

    public ApiResponse(int httpStatusCode)
    {
        Succeeded = true;
        HttpStatusCode = httpStatusCode;
    }

    public ApiResponse(int httpStatusCode, IEnumerable<ApiError> errors)
    {
        Succeeded = false;
        Errors = errors;
        HttpStatusCode = httpStatusCode;
    }

    public ApiResponse(int httpStatusCode, ApiError error)
            : this(httpStatusCode, new List<ApiError> { error })
    {
    }

    public ApiResponse()
    {
    }

    #endregion
}
