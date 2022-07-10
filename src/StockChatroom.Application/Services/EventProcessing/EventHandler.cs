using MediatR;
using StockChatroom.Application.UseCases.ChatRooms.CreateChatRoom;
using StockChatroom.Application.UseCases.Messages.ProccessCommand;
using StockChatroom.Application.UseCases.Messages.SendMessage;
using StockChatroom.Shared.Events;

namespace StockChatroom.Application.Services.EventProcessing;

public class EventHandler :
    INotificationHandler<CommandSentEvent>,
    INotificationHandler<MessageSentEvent>,
    INotificationHandler<ChatRoomCreatedEvent>
{
    private readonly IMediator _mediator;

    public EventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task Handle(CommandSentEvent notification, CancellationToken cancellationToken)
    {
        var input = new ProccessCommandInput(notification.Message, notification.ChatRoomId);
        return _mediator.Send(input, cancellationToken);
    }

    public Task Handle(MessageSentEvent notification, CancellationToken cancellationToken)
    {
        var input = new NotifyMessageInput(notification.Message, notification.MessageNotification, notification.ChatRoomId);
        return _mediator.Send(input, cancellationToken);
    }

    public Task Handle(ChatRoomCreatedEvent notification, CancellationToken cancellationToken)
    {
        var input = new NotifyChatRoomCreationInput(notification.ChatRoom);
        return _mediator.Send(input, cancellationToken);
    }
}
