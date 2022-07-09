using StockChatroom.Application.Services.Hubs;
using StockChatroom.Domain.Entities;
using StockChatroom.Infrastructure.Data;
using StockChatroom.Shared.Dtos.ChatRooms;
using System.Net;

namespace StockChatroom.Application.UseCases.ChatRooms.CreateChatRoom;

public class CreateChatRoomUseCase : ICreateChatRoomUseCase
{
    private readonly ApplicationDbContext _context;
    private readonly SignalRHub _signalRHub;

    public CreateChatRoomUseCase(ApplicationDbContext context, SignalRHub signalRHub)
    {
        _context = context;
        _signalRHub = signalRHub;
    }

    public async Task<CreateChatRoomOutput> Handle(CreateChatRoomInput request, CancellationToken cancellationToken)
    {
        var chatRoom = new ChatRoom(request.ChatRoomName);

        _ = _context.ChatRooms.Add(chatRoom);

        _ = await _context.SaveChangesAsync(cancellationToken);

        await _signalRHub.CreateChatRoomAsync(new ChatRoomDto() { Id = chatRoom.Id, Name = chatRoom.Name }, cancellationToken);

        return new CreateChatRoomOutput(HttpStatusCode.Created);
    }
}
