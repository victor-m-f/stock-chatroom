using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StockChatroom.Application.Services.AuthUser;
using StockChatroom.Application.Services.Hubs;
using StockChatroom.Application.Services.RabbitMq;
using StockChatroom.Domain.Entities;
using StockChatroom.Infrastructure.Data;
using StockChatroom.Shared.Dtos.Messages;
using System.Net;

namespace StockChatroom.Application.UseCases.Messages.SendMessage;

public class SendMessageUseCase : ISendMessageUseCase
{
    private readonly ApplicationDbContext _context;
    private readonly IAuthUser _authUser;
    private readonly SignalRHub _signalRHub;
    private readonly IMapper _mapper;
    private readonly IMessageProducer _messageProducer;

    public SendMessageUseCase(
        ApplicationDbContext context,
        IAuthUser authUser,
        SignalRHub signalRHub,
        IMapper mapper,
        IMessageProducer messageProducer)
    {
        _context = context;
        _authUser = authUser;
        _signalRHub = signalRHub;
        _mapper = mapper;
        _messageProducer = messageProducer;
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

        var chatRoom = await _context.ChatRooms.FirstOrDefaultAsync(x => x.Id == request.GroupId, cancellationToken);

        if (chatRoom == null)
        {
            var output = new SendMessageOutput(false, HttpStatusCode.NotFound);
            output.AddError("Group could not be found.");
            return output;
        }

        var message = new Message(request.MessageText, request.CreatedAt, chatRoom, fromUser);

        if (message.IsCommand)
        {

        }

        await SaveMessage();

        return new SendMessageOutput(HttpStatusCode.Created);
    }

    private void PublishCommand()
    {

    }

    private async Task SaveMessage(Message message, CancellationToken cancellationToken)
    {
        _ = _context.Messages.Add(message);
        _ = await _context.SaveChangesAsync(cancellationToken);

        await _signalRHub.SendMessageAsync(_mapper.Map<MessageDto>(message), message.ChatRoom.Id.ToString(), cancellationToken);
        await _signalRHub.SendChatNotificationAsync(
            message.ToNotification,
            message.ChatRoom.Id.ToString(),
            message.FromUserId.ToString(),
            cancellationToken);
    }
}
