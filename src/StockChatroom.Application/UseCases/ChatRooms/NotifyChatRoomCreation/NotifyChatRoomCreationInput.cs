using MediatR;
using StockChatroom.Shared.Dtos.ChatRooms;

namespace StockChatroom.Application.UseCases.ChatRooms.CreateChatRoom;

public class NotifyChatRoomCreationInput : IRequest<NotifyChatRoomCreationOutput>
{
    public ChatRoomDto ChatRoom { get; set; }

    public NotifyChatRoomCreationInput(ChatRoomDto chatRoom)
    {
        ChatRoom = chatRoom;
    }
}
