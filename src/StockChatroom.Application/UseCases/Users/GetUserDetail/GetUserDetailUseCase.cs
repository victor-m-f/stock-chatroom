using Microsoft.EntityFrameworkCore;
using StockChatroom.Infrastructure.Data;
using System.Net;

namespace StockChatroom.Application.UseCases.Users.GetUserDetail;

public class GetUserDetailUseCase : IGetUserDetailUseCase
{
    private readonly ApplicationDbContext _context;

    public GetUserDetailUseCase(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetUserDetailOutput> Handle(GetUserDetailInput request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == request.UserId, cancellationToken);

        if (user == null)
        {
            var output = new GetUserDetailOutput(false, HttpStatusCode.NotFound);
            output.AddError("User could not be found.");
            return output;
        }

        return new GetUserDetailOutput(user, HttpStatusCode.OK);
    }
}
