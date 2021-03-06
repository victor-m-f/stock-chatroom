using MediatR;
using StockChatroom.Shared.Dtos.Messages;

namespace StockChatroom.Application.UseCases.Messages.ProccessCommand;

public class ProccessCommandInput : IRequest<ProccessCommandOutput>
{
    public MessageDto Message { get; set; }
    public Guid ChatRoomId { get; set; }

    public ProccessCommandInput(MessageDto messageDto, Guid chatRoomId)
    {
        Message = messageDto;
        ChatRoomId = chatRoomId;
    }
}
