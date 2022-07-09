using Newtonsoft.Json;
using StockChatroom.Shared.ApiResponses;
using StockChatroom.Shared.Dtos.Messages.GetMessagesFromChatRoom;
using StockChatroom.Shared.Dtos.Messages.SendMessage;
using System.Net.Http.Json;
using System.Text;

namespace StockChatroom.Client.Services.Implementations;

internal class MessagesService : IMessagesService
{
    private readonly HttpClient _httpClient;

    public MessagesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<ApiResponseWithResult<GetMessagesFromChatRoomResponse>> GetMessagesFromChatRoomAsync(Guid chatRoomId)
    {
        return _httpClient.GetFromJsonAsync<ApiResponseWithResult<GetMessagesFromChatRoomResponse>>($"api/v1/ChatRooms/{chatRoomId}/Messages");
    }

    public async Task<ApiResponse> SendMessageAsync(Guid chatRoomId, SendMessageRequest request)
    {
        var httpResponse = await _httpClient.PostAsJsonAsync($"api/v1/ChatRooms/{chatRoomId}/Messages", request);
        return JsonConvert.DeserializeObject<ApiResponse>(await httpResponse.Content.ReadAsStringAsync());
    }
}
