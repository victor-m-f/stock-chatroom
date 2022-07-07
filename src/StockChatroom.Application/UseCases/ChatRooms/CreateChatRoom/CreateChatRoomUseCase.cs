using StockChatroom.Domain.Entities;
using StockChatroom.Infrastructure.Data;
using System.Net;

namespace StockChatroom.Application.UseCases.ChatRooms.CreateChatRoom;

public class CreateChatRoomUseCase : ICreateChatRoomUseCase
{
    private readonly ApplicationDbContext _context;

    public CreateChatRoomUseCase(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CreateChatRoomOutput> Handle(CreateChatRoomInput request, CancellationToken cancellationToken)
    {
        var chatRooms = new ChatRoom(request.ChatRoomName);

        _ = _context.ChatRooms.Add(chatRooms);
        _ = await _context.SaveChangesAsync(cancellationToken);

        return new CreateChatRoomOutput(HttpStatusCode.Created);
    }
}
