using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockChatroom.Application.UseCases.ChatRooms.CreateChatRoom;
using StockChatroom.Application.UseCases.ChatRooms.GetAllChatRooms;
using StockChatroom.Server.Controllers.Responses;
using StockChatroom.Shared.Dtos.ChatRooms.CreateChatRoom;
using StockChatroom.Shared.Dtos.ChatRooms.GetAllChatRooms;

namespace StockChatroom.Server.Controllers.V1;

[ApiVersion("1")]
[ApiRoute("[controller]")]
public class ChatRoomsController : ApiControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ChatRoomsController(
        ILogger<ChatRoomsController> logger,
        IMediator mediator,
        IMapper mapper)
        : base(logger)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet(Name = nameof(GetAllChatRooms))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponseWithResult<GetAllChatRoomsResponse>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<ApiResponseWithResult<GetAllChatRoomsResponse>>> GetAllChatRooms(CancellationToken cancellationToken)
    {
        using (StartUseCaseScope(nameof(GetAllChatRooms)))
        {
            var input = new GetAllChatRoomsInput();
            var output = await _mediator.Send(input, cancellationToken);

            return output.IsValid
                ? Respond(_mapper.Map<GetAllChatRoomsResponse>(output), output.StatusCode)
                : Respond<GetAllChatRoomsResponse>(output.StatusCode, output.Errors);
        }
    }

    [HttpPost(Name = nameof(CreateChatRoom))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<ApiResponse>> CreateChatRoom(CreateChatRoomRequest request, CancellationToken cancellationToken)
    {
        using (StartUseCaseScope(nameof(CreateChatRoom)))
        {
            var input = new CreateChatRoomInput(request.ChatRoomName);
            var output = await _mediator.Send(input, cancellationToken);

            return output.IsValid
                ? Respond(output.StatusCode)
                : Respond(output.StatusCode, output.Errors);
        }
    }
}
