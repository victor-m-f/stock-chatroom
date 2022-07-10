using MediatR;
using StockChatroom.Shared.Dtos.Messages;

namespace StockChatroom.Application.UseCases.Messages.SendMessage;

public class NotifyMessageInput : IRequest<NotifyMessageOutput>
{
    public MessageDto Message { get; set; }
    public string MessageNotification { get; set; }
    public Guid ChatRoomId { get; set; }

    public NotifyMessageInput(MessageDto message, string messageNotification, Guid chatRoomId)
    {
        Message = message;
        MessageNotification = messageNotification;
        ChatRoomId = chatRoomId;
    }
}
