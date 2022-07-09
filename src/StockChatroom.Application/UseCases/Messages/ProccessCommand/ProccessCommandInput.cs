using MediatR;

namespace StockChatroom.Application.UseCases.Messages.ProccessCommand;

public class ProccessCommandInput : IRequest<ProccessCommandOutput>
{
    public string Message { get; set; }

    public ProccessCommandInput(string messageDto)
    {
        Message = messageDto;
    }
}
