using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace StockChatroom.Application.Services.AuthUser;

internal class AuthUser : IAuthUser
{
    public string? Id
    {
        get
        {
            var id = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return id;
        }
    }

    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
}
