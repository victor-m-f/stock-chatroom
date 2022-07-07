using Microsoft.EntityFrameworkCore;
using StockChatroom.Application.AuthUser;
using StockChatroom.Domain.Entities;
using StockChatroom.Infrastructure.Data;
using System.Net;

namespace StockChatroom.Application.UseCases.Messages.SendMessage;

public class SendMessageUseCase : ISendMessageUseCase
{
    private readonly ApplicationDbContext _context;
    private readonly IAuthUser _authUser;

    public SendMessageUseCase(ApplicationDbContext context, IAuthUser authUser)
    {
        _context = context;
        _authUser = authUser;
    }

    public async Task<SendMessageOutput> Handle(SendMessageInput request, CancellationToken cancellationToken)
    {
        var fromUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == _authUser.Id, cancellationToken);

        if (fromUser == null)
        {
            var output = new SendMessageOutput(false, HttpStatusCode.NotFound);
            output.AddError("User could not be found.");
            return output;
        }

        var group = await _context.ChatRooms.FirstOrDefaultAsync(x => x.Id == request.GroupId, cancellationToken);

        if (group == null)
        {
            var output = new SendMessageOutput(false, HttpStatusCode.NotFound);
            output.AddError("Group could not be found.");
            return output;
        }

        var message = new Message(request.MessageText, group, fromUser);

        _ = _context.Messages.Add(message);
        _ = await _context.SaveChangesAsync(cancellationToken);

        return new SendMessageOutput(HttpStatusCode.Created);
    }
}
