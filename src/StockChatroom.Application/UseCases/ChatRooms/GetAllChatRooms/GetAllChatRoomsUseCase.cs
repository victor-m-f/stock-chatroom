using Microsoft.EntityFrameworkCore;
using StockChatroom.Infrastructure.Data;
using System.Net;

namespace StockChatroom.Application.UseCases.ChatRooms.GetAllChatRooms;

public class GetAllChatRoomsUseCase : IGetAllChatRoomsUseCase
{
    private readonly ApplicationDbContext _context;

    public GetAllChatRoomsUseCase(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetAllChatRoomsOutput> Handle(GetAllChatRoomsInput request, CancellationToken cancellationToken)
    {
        var chatRooms = await _context.ChatRooms.ToListAsync(cancellationToken);

        return new GetAllChatRoomsOutput(chatRooms, HttpStatusCode.OK);
    }
}
