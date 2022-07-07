using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace StockChatroom.Server.Controllers.Responses;

public class ApiResponse
{
    private const string LogMessageTemplate = "Response {@apiResponse} returned.";

    #region Properties

    [JsonProperty("succeeded")]
    public bool Succeeded { get; }
    [JsonProperty("errors")]
    public IEnumerable<ApiError> Errors { get; } = Enumerable.Empty<ApiError>();
    [JsonProperty("httpStatusCode")]
    public int HttpStatusCode { get; protected set; }

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

    #endregion

    public override string ToString() => JsonConvert.SerializeObject(this);

    public ObjectResult Send(ILogger logger)
    {
        Log(logger);

        return new ObjectResult(this)
        {
            StatusCode = HttpStatusCode,
        };
    }

    public Task SendAsync(ILogger logger, HttpContext context)
    {
        Log(logger);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = HttpStatusCode;
        return context.Response.WriteAsync(ToString());
    }

    private void Log(ILogger logger)
    {
        if (HttpStatusCode == (int)System.Net.HttpStatusCode.OK)
        {
            logger.LogInformation(LogMessageTemplate, this);
        }
        else if (HttpStatusCode == (int)System.Net.HttpStatusCode.InternalServerError)
        {
            logger.LogError(LogMessageTemplate, this);
        }
        else
        {
            logger.LogWarning(LogMessageTemplate, this);
        }
    }
}
