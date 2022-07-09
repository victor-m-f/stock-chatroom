using MediatR;

namespace StockChatroom.Application.UseCases.Messages.SendMessage;

public class SendMessageInput : IRequest<SendMessageOutput>
{
    public string MessageText { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid GroupId { get; set; }

    public SendMessageInput(string messageText, DateTime createdAt, Guid groupId)
    {
        MessageText = messageText;
        CreatedAt = createdAt;
        GroupId = groupId;
    }
}
