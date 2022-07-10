using AutoMapper;
using StockChatroom.Application.Services.Stooq;
using StockChatroom.Domain.Entities;
using StockChatroom.Domain.Enums;
using StockChatroom.Domain.Services;
using StockChatroom.Shared.Dtos.Messages;
using StockChatroom.Shared.Events;
using System.Net;

namespace StockChatroom.Application.UseCases.Messages.ProccessCommand;

public class ProccessCommandUseCase : IProccessCommandUseCase
{
    private readonly IMapper _mapper;
    private readonly IStooqService _stooqService;
    private readonly IMessageBrokerProducer _messageProducer;

    public ProccessCommandUseCase(
        IMapper mapper,
        IStooqService stooqService,
        IMessageBrokerProducer messageProducer)
    {
        _mapper = mapper;
        _stooqService = stooqService;
        _messageProducer = messageProducer;
    }

    public async Task<ProccessCommandOutput> Handle(ProccessCommandInput request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<Message>(request.Message).ToCommand;

        if (command.Kind == CommandKind.Stock)
        {
            await ExecuteStockCommand(command, request.ChatRoomId, cancellationToken);
        }

        return new ProccessCommandOutput(HttpStatusCode.OK);
    }

    private async Task ExecuteStockCommand(Command command, Guid chatRoomId, CancellationToken cancellationToken)
    {
        var getStocksResponse = await _stooqService.GetStocksAsync(command.Value, cancellationToken);

        var message = new MessageDto
        {
            CreatedAt = DateTime.Now,
            FromUser = new Shared.Dtos.Users.UserDto
            {
                Name = "StockChatroom Bot",
            },
            Text = getStocksResponse.Succeeded ?
            getStocksResponse.Result.FirstOrDefault().ToQuote() :
            getStocksResponse.Errors.FirstOrDefault().Message,
        };

        _messageProducer.PublishEvent(new MessageSentEvent
        {
            Message = message,
            ChatRoomId = chatRoomId,
        });
    }
}
