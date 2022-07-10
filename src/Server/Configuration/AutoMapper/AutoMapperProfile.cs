using AutoMapper;
using StockChatroom.Application.UseCases.ChatRooms.GetAllChatRooms;
using StockChatroom.Application.UseCases.ChatRooms.GetChatRoomDetail;
using StockChatroom.Application.UseCases.Messages.GetMessagesFromChatRoom;
using StockChatroom.Application.UseCases.Users.GetAllUsers;
using StockChatroom.Application.UseCases.Users.GetUserDetail;
using StockChatroom.Domain.Entities;
using StockChatroom.Shared.Dtos.ChatRooms;
using StockChatroom.Shared.Dtos.ChatRooms.GetAllChatRooms;
using StockChatroom.Shared.Dtos.ChatRooms.GetChatRoomDetail;
using StockChatroom.Shared.Dtos.Messages;
using StockChatroom.Shared.Dtos.Messages.GetMessagesFromChatRoom;
using StockChatroom.Shared.Dtos.Users;
using StockChatroom.Shared.Dtos.Users.GetAllUsers;
using StockChatroom.Shared.Dtos.Users.GetUserDetail;

namespace StockChatroom.Server.Configuration.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        MapUsers();
        MapMessages();
        MapChatRooms();
    }

    private void MapUsers()
    {
        _ = CreateMap<GetAllUsersOutput, GetAllUsersResponse>();
        _ = CreateMap<GetUserDetailOutput, GetUserDetailResponse>();
        _ = CreateMap<ApplicationUser, UserDto>()
            .ForMember(dest => dest.Name, opt =>
            opt.MapFrom(src => src.Email));
    }

    private void MapMessages()
    {
        _ = CreateMap<GetMessagesFromChatRoomOutput, GetMessagesFromChatRoomResponse>();
        _ = CreateMap<Message, MessageDto>().ReverseMap();
    }

    private void MapChatRooms()
    {
        _ = CreateMap<GetAllChatRoomsOutput, GetAllChatRoomsResponse>();
        _ = CreateMap<GetChatRoomDetailOutput, GetChatRoomDetailResponse>();
        _ = CreateMap<ChatRoom, ChatRoomDto>();
    }
}
