using MediatR;

namespace StockChatroom.Application.UseCases.ChatRooms.CreateChatRoom;

public class CreateChatRoomInput : IRequest<CreateChatRoomOutput>
{
    public string ChatRoomName { get; set; }

    public CreateChatRoomInput(string chatRoomName)
    {
        ChatRoomName = chatRoomName;
    }
}
