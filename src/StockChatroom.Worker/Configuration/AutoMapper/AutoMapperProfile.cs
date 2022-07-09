using AutoMapper;
using StockChatroom.Domain.Entities;
using StockChatroom.Shared.Dtos.Messages;

namespace StockChatroom.Worker.Configuration.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        MapMessages();
    }

    private void MapMessages()
    {
        _ = CreateMap<MessageDto, Message>();
    }
}
