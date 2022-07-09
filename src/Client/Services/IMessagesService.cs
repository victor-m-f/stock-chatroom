using StockChatroom.Shared.ApiResponses;
using StockChatroom.Shared.Dtos.Messages.GetMessagesFromChatRoom;
using StockChatroom.Shared.Dtos.Messages.SendMessage;

namespace StockChatroom.Client.Services;

public interface IMessagesService
{
    public Task<ApiResponseWithResult<GetMessagesFromChatRoomResponse>> GetMessagesFromChatRoomAsync(Guid chatRoomId);
    public Task<ApiResponse> SendMessageAsync(Guid chatRoomId, SendMessageRequest request);
}
