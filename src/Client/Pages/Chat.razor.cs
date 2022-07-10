using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR.Client;
using StockChatroom.Shared.Dtos.ChatRooms;
using StockChatroom.Shared.Dtos.ChatRooms.CreateChatRoom;
using StockChatroom.Shared.Dtos.Messages;
using StockChatroom.Shared.Dtos.Messages.SendMessage;

namespace StockChatroom.Client.Pages;

public partial class Chat
{
    [CascadingParameter]
    public HubConnection HubConnection { get; set; }
    [Parameter]
    public string CurrentMessage { get; set; }
    [Parameter]
    public string CurrentUserId { get; set; }
    [Parameter]
    public string CurrentUserEmail { get; set; }
    public List<ChatRoomDto>? ChatRooms { get; set; } = new List<ChatRoomDto>();
    [Parameter]
    public string ChatRoomName { get; set; }
    [Parameter]
    public string ChatRoomId { get; set; }
    private List<MessageDto> Messages { get; set; } = new List<MessageDto>();

    protected override async Task OnInitializedAsync()
    {
        if (!await IsAuthorized())
        {
            _navigationManager.NavigateTo($"authentication/login?returnUrl={Uri.EscapeDataString(_navigationManager.Uri)}");
            return;
        }

        await SetupHubConnection();

        ChatRooms = (await _stockChatRoomService.ChatRooms.GetAllChatRoomsAsync()).Result.ChatRooms;

        if (string.IsNullOrEmpty(ChatRoomId))
        {
            ChatRoomId = ChatRooms.FirstOrDefault().Id.ToString();
        }

        if (!string.IsNullOrEmpty(ChatRoomId))
        {
            await LoadChatRoom(ChatRoomId);
            StateHasChanged();
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender) =>
        _ = await _jsRuntime.InvokeAsync<string>("ScrollToBottom", new object[] { "chatContainer" });

    private async Task<bool> IsAuthorized()
    {
        var state = await _stateProvider.GetAuthenticationStateAsync();
        var user = state.User;

        CurrentUserId = user.Claims.Where(a => a.Type == "sub").Select(a => a.Value).FirstOrDefault();
        CurrentUserEmail = user.Claims.Where(a => a.Type == "name").Select(a => a.Value).FirstOrDefault();

        return !string.IsNullOrEmpty(CurrentUserId);
    }

    private async Task SetupHubConnection()
    {
        if (HubConnection == null)
        {
            HubConnection = new HubConnectionBuilder().WithUrl(new Uri("https://localhost:7236/signalRHub")).Build();
        }

        if (HubConnection.State == HubConnectionState.Disconnected)
        {
            await HubConnection.StartAsync();
        }

        SetupOnReceiveMessage();
        SetupOnCreateChatRoom();
    }

    private async Task SetupOnReceiveMessage()
    {
        _ = HubConnection.On<MessageDto, string>("ReceiveMessage", async (message, toChatRoomId) =>
        {
            if (toChatRoomId == ChatRoomId)
            {
                AddMessage(message);
            }

            _ = await _jsRuntime.InvokeAsync<string>("ScrollToBottom", new object[] { "chatContainer" });
            StateHasChanged();
        });
    }

    private async Task SetupOnCreateChatRoom()
    {
        _ = HubConnection.On<ChatRoomDto>("CreateChatRoom", async (chatRoomDto) =>
        {
            AddChatRoom(chatRoomDto);
            StateHasChanged();
        });
    }

    private async Task LoadChatRoom(string chatRoomId)
    {
        var getChatRoomDetailResponse = await _stockChatRoomService.ChatRooms.GetChatRoomDetailAsync(new Guid(chatRoomId));
        ChatRoomId = getChatRoomDetailResponse.Result.ChatRoom.Id.ToString();
        ChatRoomName = getChatRoomDetailResponse.Result.ChatRoom.Name;
        _navigationManager.NavigateTo($"chat/{ChatRoomId}");
        Messages = getChatRoomDetailResponse.Result.ChatRoom.Messages;
    }

    private void AddMessage(MessageDto message)
    {
        if (Messages.Count == 50)
        {
            Messages.RemoveAt(0);
        }

        Messages.Add(message);
    }

    private void AddChatRoom(ChatRoomDto chatRoom)
    {
        ChatRooms.Add(chatRoom);
    }

    private async Task SubmitAsync()
    {
        if (!string.IsNullOrEmpty(CurrentMessage) && !string.IsNullOrEmpty(ChatRoomId))
        {
            var sendMessageRequest = new SendMessageRequest()
            {
                MessageText = CurrentMessage,
                CreatedAt = DateTime.Now,
            };

            var sendMessageResult = await _stockChatRoomService.Messages.SendMessageAsync(new Guid(ChatRoomId), sendMessageRequest);

            if (sendMessageResult.Succeeded)
            {
                CurrentMessage = string.Empty;
            }
        }
    }

    private async Task CreateChatRoomAsync()
    {
        var sendMessageRequest = new CreateChatRoomRequest()
        {
            ChatRoomName = "Chat " + Guid.NewGuid().ToString()[^5..],
        };

        _ = await _stockChatRoomService.ChatRooms.CreateChatRoomAsync(sendMessageRequest);
    }
}
