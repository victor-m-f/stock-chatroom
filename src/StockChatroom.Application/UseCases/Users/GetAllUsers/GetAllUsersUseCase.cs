using Microsoft.EntityFrameworkCore;
using StockChatroom.Application.Services.AuthUser;
using StockChatroom.Infrastructure.Data;
using System.Net;

namespace StockChatroom.Application.UseCases.Users.GetAllUsers;

public class GetAllUsersUseCase : IGetAllUsersUseCase
{
    private readonly ApplicationDbContext _context;
    private readonly IAuthUser _authUser;

    public GetAllUsersUseCase(ApplicationDbContext context, IAuthUser authUser)
    {
        _context = context;
        _authUser = authUser;
    }

    public async Task<GetAllUsersOutput> Handle(GetAllUsersInput request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(_authUser.Id))
        {
            var output = new GetAllUsersOutput(false, HttpStatusCode.Unauthorized);
            output.AddError("User must be logged in before performing this action.");
            return output;
        }

        var allUsers = await _context.Users.Where(user => user.Id != _authUser.Id).ToListAsync(cancellationToken);

        return new GetAllUsersOutput(allUsers, HttpStatusCode.OK);
    }
}
