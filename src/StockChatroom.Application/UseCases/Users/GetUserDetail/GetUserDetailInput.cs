using MediatR;

namespace StockChatroom.Application.UseCases.Users.GetUserDetail;

public class GetUserDetailInput : IRequest<GetUserDetailOutput>
{
    public string UserId { get; set; }

    public GetUserDetailInput(string userId)
    {
        UserId = userId;
    }
}
