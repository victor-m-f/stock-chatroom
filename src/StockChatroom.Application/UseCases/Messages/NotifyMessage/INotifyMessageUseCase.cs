using MediatR;

namespace StockChatroom.Application.UseCases.Messages.SendMessage;

public interface INotifyMessageUseCase : IRequestHandler<NotifyMessageInput, NotifyMessageOutput>
{
}
