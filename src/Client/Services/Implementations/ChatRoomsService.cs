using Newtonsoft.Json;
using StockChatroom.Shared.ApiResponses;
using StockChatroom.Shared.Dtos.ChatRooms.CreateChatRoom;
using StockChatroom.Shared.Dtos.ChatRooms.GetAllChatRooms;
using StockChatroom.Shared.Dtos.ChatRooms.GetChatRoomDetail;
using System.Net.Http.Json;
using System.Text;

namespace StockChatroom.Client.Services.Implementations;

internal class ChatRoomsService : IChatRoomsService
{
    private readonly HttpClient _httpClient;

    public ChatRoomsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiResponseWithResult<GetAllChatRoomsResponse>> GetAllChatRoomsAsync()
    {
        return await _httpClient.GetFromJsonAsync<ApiResponseWithResult<GetAllChatRoomsResponse>>($"api/v1/ChatRooms");
    }

    public Task<ApiResponseWithResult<GetChatRoomDetailResponse>> GetChatRoomDetailAsync(Guid chatRoomId)
    {
        return _httpClient.GetFromJsonAsync<ApiResponseWithResult<GetChatRoomDetailResponse>>($"api/v1/ChatRooms/{chatRoomId}");
    }

    public async Task<ApiResponse> CreateChatRoomAsync(CreateChatRoomRequest request)
    {
        var httpResponse = await _httpClient.PostAsJsonAsync($"api/v1/ChatRooms", request);
        return JsonConvert.DeserializeObject<ApiResponse>(await httpResponse.Content.ReadAsStringAsync());
    }
}
