using Microsoft.EntityFrameworkCore;
using StockChatroom.Infrastructure.Data;
using System.Net;

namespace StockChatroom.Application.UseCases.Messages.GetMessagesFromChatRoom;

public class GetMessagesFromChatRoomUseCase : IGetMessagesFromChatRoomUseCase
{
    private readonly ApplicationDbContext _context;

    public GetMessagesFromChatRoomUseCase(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetMessagesFromChatRoomOutput> Handle(GetMessagesFromChatRoomInput request, CancellationToken cancellationToken)
    {
        var messages = await _context.Messages
                .Where(x => x.ChatRoomId == request.ChatRoomId)
                .OrderBy(x => x.CreatedAt)
                .Include(a => a.FromUser)
                .ToListAsync(cancellationToken);

        return new GetMessagesFromChatRoomOutput(messages, HttpStatusCode.OK);
    }
}
