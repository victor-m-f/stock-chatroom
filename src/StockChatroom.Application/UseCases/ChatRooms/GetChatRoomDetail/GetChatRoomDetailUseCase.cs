using Microsoft.EntityFrameworkCore;
using StockChatroom.Infrastructure.Data;
using System.Net;

namespace StockChatroom.Application.UseCases.ChatRooms.GetChatRoomDetail;

public class GetChatRoomDetailUseCase : IGetChatRoomDetailUseCase
{
    private readonly ApplicationDbContext _context;

    public GetChatRoomDetailUseCase(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetChatRoomDetailOutput> Handle(GetChatRoomDetailInput request, CancellationToken cancellationToken)
    {
        var chatRoom = await _context.ChatRooms
            .FirstOrDefaultAsync(x => x.Id == request.ChatRoomId, cancellationToken);

        if (chatRoom == null)
        {
            var output = new GetChatRoomDetailOutput(false, HttpStatusCode.NotFound);
            output.AddError("ChatRoom could not be found.");
            return output;
        }

        chatRoom.Messages = await _context.Messages
            .Include(x => x.FromUser)
            .OrderByDescending(x => x.CreatedAt)
            .Where(x => x.ChatRoomId == request.ChatRoomId)
            .Take(50)
            .ToListAsync(cancellationToken);

        chatRoom.Messages = chatRoom.Messages.OrderBy(x => x.CreatedAt).ToList();

        return new GetChatRoomDetailOutput(chatRoom, HttpStatusCode.OK);
    }
}
