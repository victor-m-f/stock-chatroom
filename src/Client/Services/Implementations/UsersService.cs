using StockChatroom.Shared.ApiResponses;
using StockChatroom.Shared.Dtos.Users.GetAllUsers;
using StockChatroom.Shared.Dtos.Users.GetUserDetail;
using System.Net.Http.Json;

namespace StockChatroom.Client.Services.Implementations;

internal class UsersService : IUsersService
{
    private readonly HttpClient _httpClient;

    public UsersService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiResponseWithResult<GetUserDetailResponse>> GetUserDetailAsync(string userId)
    {
        return await _httpClient.GetFromJsonAsync<ApiResponseWithResult<GetUserDetailResponse>>($"api/v1/Users/{userId}");
    }

    public async Task<ApiResponseWithResult<GetAllUsersResponse>> GetUsersAsync()
    {
        return await _httpClient.GetFromJsonAsync<ApiResponseWithResult<GetAllUsersResponse>>("api/v1/Users");
    }
}
