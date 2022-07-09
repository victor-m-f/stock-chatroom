using StockChatroom.Shared.ApiResponses;

namespace StockChatroom.Server.Middlewares;

public class ErrorMiddleware
{
    private const string LogResponseTemplate = "Response {@apiResponse} returned.";

    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorMiddleware> _logger;

    public ErrorMiddleware(RequestDelegate next, ILogger<ErrorMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var error = new ApiError(exception.Message);

        var apiResponse = new ApiResponse(500, error);
        _logger.LogError(LogResponseTemplate, apiResponse);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 500;
        return context.Response.WriteAsync(apiResponse.ToString());
    }
}
