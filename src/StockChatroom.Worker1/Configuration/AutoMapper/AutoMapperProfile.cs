using AutoMapper;
using StockChatroom.Domain.Entities;
using StockChatroom.Shared.Dtos.Messages;
using StockChatroom.Shared.Dtos.Users;

namespace StockChatroom.Worker.Configuration.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        MapUsers();
        MapMessages();
    }

    private void MapUsers()
    {
        _ = CreateMap<UserDto, ApplicationUser>()
            .ForMember(dest => dest.Email, opt =>
            opt.MapFrom(src => src.Name));
    }

    private void MapMessages()
    {
        _ = CreateMap<MessageDto, Message>();
    }
}
