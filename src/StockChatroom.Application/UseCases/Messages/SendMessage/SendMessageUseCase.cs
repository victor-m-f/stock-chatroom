using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StockChatroom.Application.Services.AuthUser;
using StockChatroom.Domain.Entities;
using StockChatroom.Domain.Services;
using StockChatroom.Infrastructure.Data;
using StockChatroom.Shared.Dtos.Messages;
using StockChatroom.Shared.Events;
using System.Net;

namespace StockChatroom.Application.UseCases.Messages.SendMessage;

public class SendMessageUseCase : ISendMessageUseCase
{
    private readonly ApplicationDbContext _context;
    private readonly IAuthUser _authUser;
    private readonly IMapper _mapper;
    private readonly IMessageBrokerProducer _messageProducer;

    public SendMessageUseCase(
        ApplicationDbContext context,
        IAuthUser authUser,
        IMapper mapper,
        IMessageBrokerProducer messageProducer)
    {
        _context = context;
        _authUser = authUser;
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

        var chatRoom = await _context.ChatRooms.FirstOrDefaultAsync(x => x.Id == request.ChatRoomId, cancellationToken);

        if (chatRoom == null)
        {
            var output = new SendMessageOutput(false, HttpStatusCode.NotFound);
            output.AddError("Group could not be found.");
            return output;
        }

        var message = new Message(request.MessageText, request.CreatedAt, chatRoom, fromUser);

        if (!message.IsCommand)
        {
            await SaveMessage(message, cancellationToken);
        }

        PublishMessage(message);

        if (message.IsCommand)
        {
            PublishCommand(message);
        }

        return new SendMessageOutput(HttpStatusCode.Created);
    }

    private void PublishCommand(Message message) =>
        _messageProducer.PublishEvent(new CommandSentEvent() { Message = _mapper.Map<MessageDto>(message), ChatRoomId = message.ChatRoom.Id });

    private void PublishMessage(Message message) =>
        _messageProducer.PublishEvent(new MessageSentEvent()
        {
            Message = _mapper.Map<MessageDto>(message),
            MessageNotification = message.ToNotification,
            ChatRoomId = message.ChatRoom.Id,
        });

    private async Task SaveMessage(Message message, CancellationToken cancellationToken)
    {
        _ = _context.Messages.Add(message);
        _ = await _context.SaveChangesAsync(cancellationToken);
    }
}
