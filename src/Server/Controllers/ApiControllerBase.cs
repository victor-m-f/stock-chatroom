using Microsoft.AspNetCore.Mvc;
using StockChatroom.Server.Controllers.Responses;

namespace StockChatroom.Server.Controllers;

[ApiController]
[ApiRoute("[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private readonly ILogger _logger;

    protected ApiControllerBase(ILogger logger)
    {
        _logger = logger;
    }

    protected ActionResult<ApiResponse> Respond(int httpStatusCode)
    {
        var response = new ApiResponse(httpStatusCode);
        return response.Send(_logger);
    }

    protected ActionResult<ApiResponse> Respond(int httpStatusCode, IEnumerable<string> errorMessages)
    {
        var response = new ApiResponse(httpStatusCode, errorMessages.Select(x => new ApiError(x)));
        return response.Send(_logger);
    }

    protected ActionResult<ApiResponseWithResult<T>> Respond<T>(T result, int httpStatusCode)
        where T : class
    {
        var response = new ApiResponseWithResult<T>(result, httpStatusCode);
        return response.Send(_logger);
    }

    protected ActionResult<ApiResponseWithResult<T>> Respond<T>(int httpStatusCode, IEnumerable<string> errorMessages)
        where T : class
    {
        var response = new ApiResponseWithResult<T>(errorMessages.Select(x => new ApiError(x)), httpStatusCode);
        return response.Send(_logger);
    }

    protected IDisposable StartUseCaseScope(string useCaseName) =>
        _logger.BeginScope(new Dictionary<string, string> { { "UseCase", useCaseName } });
}
