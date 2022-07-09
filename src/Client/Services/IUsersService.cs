using StockChatroom.Shared.ApiResponses;
using StockChatroom.Shared.Dtos.Users.GetAllUsers;
using StockChatroom.Shared.Dtos.Users.GetUserDetail;

namespace StockChatroom.Client.Services;

public interface IUsersService
{
    public Task<ApiResponseWithResult<GetUserDetailResponse>> GetUserDetailAsync(string userId);
    public Task<ApiResponseWithResult<GetAllUsersResponse>> GetUsersAsync();
}
