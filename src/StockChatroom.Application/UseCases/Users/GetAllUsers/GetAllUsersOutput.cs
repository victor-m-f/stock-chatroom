using StockChatroom.Domain.Entities;
using System.Net;

namespace StockChatroom.Application.UseCases.Users.GetAllUsers;

public class GetAllUsersOutput : OutputBase
{
    public List<ApplicationUser>? AllUsers { get; }

    public GetAllUsersOutput(List<ApplicationUser> allUsers, HttpStatusCode httpStatusCode)
        : base(httpStatusCode)
    {
        AllUsers = allUsers;
    }

    public GetAllUsersOutput(bool isValid, HttpStatusCode httpStatusCode)
        : base(isValid, httpStatusCode)
    {
    }
}
