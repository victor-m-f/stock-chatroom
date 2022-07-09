using MediatR;

namespace StockChatroom.Application.UseCases.Messages.ProccessCommand;

public interface IProccessCommandUseCase : IRequestHandler<ProccessCommandInput, ProccessCommandOutput>
{
}
