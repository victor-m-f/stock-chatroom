using StockChatroom.Domain.Entities;
using System.Net;

namespace StockChatroom.Application.UseCases.Users.GetUserDetail;

public class GetUserDetailOutput : OutputBase
{
    public ApplicationUser? User { get; }

    public GetUserDetailOutput(ApplicationUser user, HttpStatusCode httpStatusCode)
        : base(httpStatusCode)
    {
        User = user;
    }

    public GetUserDetailOutput(bool isValid, HttpStatusCode httpStatusCode)
        : base(isValid, httpStatusCode)
    {
    }
}
