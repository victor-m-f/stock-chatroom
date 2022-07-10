using AutoMapper;
using StockChatroom.Domain.Entities;
using StockChatroom.Domain.Services;
using StockChatroom.Infrastructure.Data;
using StockChatroom.Shared.Dtos.ChatRooms;
using StockChatroom.Shared.Events;
using System.Net;

namespace StockChatroom.Application.UseCases.ChatRooms.CreateChatRoom;

public class CreateChatRoomUseCase : ICreateChatRoomUseCase
{
    private readonly ApplicationDbContext _context;
    private readonly IMessageBrokerProducer _messageProducer;
    private readonly IMapper _mapper;

    public CreateChatRoomUseCase(
        ApplicationDbContext context,
        IMessageBrokerProducer messageProducer,
        IMapper mapper)
    {
        _context = context;
        _messageProducer = messageProducer;
        _mapper = mapper;
    }

    public async Task<CreateChatRoomOutput> Handle(CreateChatRoomInput request, CancellationToken cancellationToken)
    {
        var chatRoom = new ChatRoom(request.ChatRoomName);

        _ = _context.ChatRooms.Add(chatRoom);

        _ = await _context.SaveChangesAsync(cancellationToken);

        _messageProducer.PublishEvent(new ChatRoomCreatedEvent
        {
            ChatRoom = _mapper.Map<ChatRoomDto>(chatRoom),
        });

        return new CreateChatRoomOutput(HttpStatusCode.Created);
    }
}
