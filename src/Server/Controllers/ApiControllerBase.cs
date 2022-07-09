using Microsoft.AspNetCore.Mvc;
using StockChatroom.Shared.ApiResponses;

namespace StockChatroom.Server.Controllers;

[ApiController]
[ApiRoute("[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private const string LogResponseTemplate = "Response {@apiResponse} returned.";

    private readonly ILogger _logger;

    protected ApiControllerBase(ILogger logger)
    {
        _logger = logger;
    }

    protected ActionResult<ApiResponse> Respond(int httpStatusCode)
    {
        var response = new ApiResponse(httpStatusCode);
        return Send(response);
    }

    protected ActionResult<ApiResponse> Respond(int httpStatusCode, IEnumerable<string> errorMessages)
    {
        var response = new ApiResponse(httpStatusCode, errorMessages.Select(x => new ApiError(x)));
        return Send(response);
    }

    protected ActionResult<ApiResponseWithResult<T>> Respond<T>(T result, int httpStatusCode)
        where T : class
    {
        var response = new ApiResponseWithResult<T>(result, httpStatusCode);
        return Send(response);
    }

    protected ActionResult<ApiResponseWithResult<T>> Respond<T>(int httpStatusCode, IEnumerable<string> errorMessages)
        where T : class
    {
        var response = new ApiResponseWithResult<T>(errorMessages.Select(x => new ApiError(x)), httpStatusCode);
        return Send(response);
    }

    protected IDisposable StartUseCaseScope(string useCaseName) =>
        _logger.BeginScope(new Dictionary<string, string> { { "UseCase", useCaseName } });

    private ObjectResult Send(ApiResponse response)
    {
        Log(response);

        return new ObjectResult(response)
        {
            StatusCode = response.HttpStatusCode,
        };
    }

    private void Log(ApiResponse response)
    {
        if (response.HttpStatusCode > 500)
        {
            _logger.LogError(LogResponseTemplate, response);
            return;
        }

        _logger.LogInformation(LogResponseTemplate, response);
    }
}
