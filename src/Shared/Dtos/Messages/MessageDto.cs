using StockChatroom.Shared.Dtos.Users;

namespace StockChatroom.Shared.Dtos.Messages;

public class MessageDto
{
    public long Id { get; set; }
    public string MessageText { get; set; }
    public string ToUserId { get; set; }
    public string FromUserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserDto? FromUser { get; set; }
}
