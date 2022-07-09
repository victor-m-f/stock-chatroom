using StockChatroom.Domain.Entities;
using System.Net;

namespace StockChatroom.Application.UseCases.Users.GetAllUsers;

public class GetAllUsersOutput : OutputBase
{
    public List<ApplicationUser>? Users { get; }

    public GetAllUsersOutput(List<ApplicationUser> users, HttpStatusCode httpStatusCode)
        : base(httpStatusCode)
    {
        Users = users;
    }

    public GetAllUsersOutput(bool isValid, HttpStatusCode httpStatusCode)
        : base(isValid, httpStatusCode)
    {
    }
}
