using MediatR;

namespace StockChatroom.Application.UseCases.Messages.SendMessage;

public class SendMessageInput : IRequest<SendMessageOutput>
{
    public string MessageText { get; set; }
    public Guid GroupId { get; set; }

    public SendMessageInput(string messageText, Guid groupId)
    {
        MessageText = messageText;
        GroupId = groupId;
    }
}
