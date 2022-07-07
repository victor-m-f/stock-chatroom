using MediatR;

namespace StockChatroom.Application.UseCases.Users.GetUserDetail;

public interface IGetUserDetailUseCase : IRequestHandler<GetUserDetailInput, GetUserDetailOutput>
{
}
