using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockChatroom.Application.UseCases.Users.GetAllUsers;
using StockChatroom.Application.UseCases.Users.GetUserDetail;
using StockChatroom.Shared.ApiResponses;
using StockChatroom.Shared.Dtos.Users.GetAllUsers;
using StockChatroom.Shared.Dtos.Users.GetUserDetail;

namespace StockChatroom.Server.Controllers.V1;

[ApiVersion("1")]
[ApiRoute("[controller]")]
public class UsersController : ApiControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UsersController(
        ILogger<UsersController> logger,
        IMediator mediator,
        IMapper mapper)
        : base(logger)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet(Name = nameof(GetAllUsers))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponseWithResult<GetAllUsersResponse>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<ApiResponseWithResult<GetAllUsersResponse>>> GetAllUsers(CancellationToken cancellationToken)
    {
        using (StartUseCaseScope(nameof(GetAllUsers)))
        {
            var input = new GetAllUsersInput();
            var output = await _mediator.Send(input, cancellationToken);

            return output.IsValid
                ? Respond(_mapper.Map<GetAllUsersResponse>(output), output.StatusCode)
                : Respond<GetAllUsersResponse>(output.StatusCode, output.Errors);
        }
    }

    [HttpGet("{userId}", Name = nameof(GetUserDetail))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponseWithResult<GetUserDetailResponse>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<ApiResponseWithResult<GetUserDetailResponse>>> GetUserDetail(string userId, CancellationToken cancellationToken)
    {
        using (StartUseCaseScope(nameof(GetUserDetail)))
        {
            var input = new GetUserDetailInput(userId);
            var output = await _mediator.Send(input, cancellationToken);

            return output.IsValid
                ? Respond(_mapper.Map<GetUserDetailResponse>(output), output.StatusCode)
                : Respond<GetUserDetailResponse>(output.StatusCode, output.Errors);
        }
    }
}
