using StockChatroom.Shared.ApiResponses;
using StockChatroom.Shared.Dtos.ChatRooms.CreateChatRoom;
using StockChatroom.Shared.Dtos.ChatRooms.GetAllChatRooms;
using StockChatroom.Shared.Dtos.ChatRooms.GetChatRoomDetail;

namespace StockChatroom.Client.Services;

public interface IChatRoomsService
{
    public Task<ApiResponseWithResult<GetAllChatRoomsResponse>> GetAllChatRoomsAsync();
    public Task<ApiResponseWithResult<GetChatRoomDetailResponse>> GetChatRoomDetailAsync(Guid chatRoomId);
    public Task<ApiResponse> CreateChatRoomAsync(CreateChatRoomRequest request);
}
