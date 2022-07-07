using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StockChatroom.Application.AuthUser;
using StockChatroom.Application.UseCases.ChatRooms.CreateChatRoom;
using StockChatroom.Application.UseCases.ChatRooms.GetAllChatRooms;
using StockChatroom.Application.UseCases.Messages.GetMessagesFromChatRoom;
using StockChatroom.Application.UseCases.Messages.SendMessage;
using StockChatroom.Application.UseCases.Users.GetAllUsers;
using StockChatroom.Application.UseCases.Users.GetUserDetail;

namespace StockChatroom.Application.Configuration;

public static class ApplicationConfiguration
{
    public static void ConfigureApplication(this IServiceCollection services) =>
        services.InjectDependencies();

    private static void InjectDependencies(this IServiceCollection services)
    {
        _ = services.AddHttpContextAccessor();
        _ = services.AddScoped<IAuthUser, AuthUser.AuthUser>();

        // Users
        services.AddUseCase<GetAllUsersInput, GetAllUsersOutput, GetAllUsersUseCase>();
        services.AddUseCase<GetUserDetailInput, GetUserDetailOutput, GetUserDetailUseCase>();

        // Messages
        services.AddUseCase<SendMessageInput, SendMessageOutput, SendMessageUseCase>();
        services.AddUseCase<GetMessagesFromChatRoomInput, GetMessagesFromChatRoomOutput, GetMessagesFromChatRoomUseCase>();

        // ChatRooms
        services.AddUseCase<GetAllChatRoomsInput, GetAllChatRoomsOutput, GetAllChatRoomsUseCase>();
        services.AddUseCase<CreateChatRoomInput, CreateChatRoomOutput, CreateChatRoomUseCase>();
    }

    /// <summary>
    /// Adds a command and the handler that will manage it.
    /// </summary>
    /// <typeparam name="TInput">The input to be handled.</typeparam>
    /// <typeparam name="TOutput">The expected response after handling the input.</typeparam>
    /// <typeparam name="TCommandHandler">The <see cref="CommandHandlerBase"/> that will handle the command.</typeparam>
    /// <param name="services">The collection of services where this command will be available.</param>
    private static void AddUseCase<TInput, TOutput, TCommandHandler>(this IServiceCollection services)
        where TInput : IRequest<TOutput>
        where TCommandHandler : class, IRequestHandler<TInput, TOutput> =>
        _ = services.AddScoped<IRequestHandler<TInput, TOutput>, TCommandHandler>();
}
