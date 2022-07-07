using Microsoft.AspNetCore.Mvc;

namespace StockChatroom.Server.Controllers;

public class ApiRouteAttribute : RouteAttribute
{
    private const string BaseRoute = "api/v{version:apiVersion}/";

    public ApiRouteAttribute(string template)
        : base(BaseRoute + template)
    {
    }
}
