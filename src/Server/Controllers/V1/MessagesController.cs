using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockChatroom.Application.UseCases.Messages.GetMessagesFromChatRoom;
using StockChatroom.Application.UseCases.Messages.SendMessage;
using StockChatroom.Server.Controllers.Responses;
using StockChatroom.Shared.Dtos.Messages.GetMessagesFromChatRoom;
using StockChatroom.Shared.Dtos.Messages.SendMessage;

namespace StockChatroom.Server.Controllers.V1;

[ApiVersion("1")]
[ApiRoute("ChatRooms/{chatRoomId:guid}/[controller]")]
public class MessagesController : ApiControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public MessagesController(ILogger<MessagesController> logger, IMediator mediator, IMapper mapper)
        : base(logger)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost(Name = nameof(SendMessage))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<ApiResponse>> SendMessage(Guid chatRoomId, SendMessageRequest request, CancellationToken cancellationToken)
    {
        using (StartUseCaseScope(nameof(SendMessage)))
        {
            var input = new SendMessageInput(request.MessageText, chatRoomId);
            var output = await _mediator.Send(input, cancellationToken);

            return output.IsValid
                ? Respond(output.StatusCode)
                : Respond(output.StatusCode, output.Errors);
        }
    }

    [HttpGet(Name = nameof(GetMessagesFromChatRoom))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponseWithResult<GetMessagesFromChatRoomResponse>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<ApiResponseWithResult<GetMessagesFromChatRoomResponse>>> GetMessagesFromChatRoom(Guid chatRoomId, CancellationToken cancellationToken)
    {
        using (StartUseCaseScope(nameof(GetMessagesFromChatRoom)))
        {
            var input = new GetMessagesFromChatRoomInput(chatRoomId);
            var output = await _mediator.Send(input, cancellationToken);

            return output.IsValid
                ? Respond(_mapper.Map<GetMessagesFromChatRoomResponse>(output), output.StatusCode)
                : Respond<GetMessagesFromChatRoomResponse>(output.StatusCode, output.Errors);
        }
    }
}
