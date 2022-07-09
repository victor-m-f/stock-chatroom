using AutoMapper;
using StockChatroom.Application.Services.Stooq;
using StockChatroom.Application.UseCases.Messages.SendMessage;
using StockChatroom.Domain.Entities;
using StockChatroom.Domain.Enums;
using System.Net;

namespace StockChatroom.Application.UseCases.Messages.ProccessCommand;

public class ProccessCommandUseCase : IProccessCommandUseCase
{
    private readonly IMapper _mapper;
    private readonly IStooqService _stooqService;

    public ProccessCommandUseCase(
        IMapper mapper,
        IStooqService stooqService)
    {
        _mapper = mapper;
        _stooqService = stooqService;
    }

    public async Task<ProccessCommandOutput> Handle(ProccessCommandInput request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<Message>(request.Message).ToCommand;

        if (command.Kind == CommandKind.Stock)
        {
            await ExecuteStockCommand(command, cancellationToken);
        }

        return new ProccessCommandOutput(HttpStatusCode.OK);
    }

    private async Task ExecuteStockCommand(Command command, CancellationToken cancellationToken)
    {
        var getStocksResponse = await _stooqService.GetStocksAsync(command.Value, cancellationToken);
    }
}
