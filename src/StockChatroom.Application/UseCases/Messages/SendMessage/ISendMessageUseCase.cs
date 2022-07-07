using MediatR;

namespace StockChatroom.Application.UseCases.Messages.SendMessage;

public interface ISendMessageUseCase : IRequestHandler<SendMessageInput, SendMessageOutput>
{
}
