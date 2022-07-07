namespace StockChatroom.Server.Controllers;

public class ApiRequest
{
    #region Properties

    public string HttpMethod { get; }
    public string Route { get; }
    public string QueryString { get; }

    /// <summary>
    /// Gets the value of Content-Type http header, which indicates the media type of the entity-body.
    /// </summary>
    public string ContentType { get; }

    /// <summary>
    /// Gets the value of Content-Length http header, which indicates the transfer-length of the message-body.
    /// </summary>
    public long? ContentLength { get; }

    #endregion

    private ApiRequest(HttpRequest httpRequest)
    {
        HttpMethod = httpRequest.Method;
        Route = httpRequest.Path.Value!;
        QueryString = httpRequest.QueryString.Value!;
        ContentType = httpRequest.ContentType!;
        ContentLength = httpRequest.ContentLength;
    }

    public static void Register(ILogger logger, HttpRequest httpRequest)
    {
        var request = new ApiRequest(httpRequest);
        logger.LogInformation("Request {@request} received", request);
    }
}
