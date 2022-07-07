using MediatR;

namespace StockChatroom.Application.UseCases.Users.GetAllUsers;

public interface IGetAllUsersUseCase : IRequestHandler<GetAllUsersInput, GetAllUsersOutput>
{
}
