using StockChatroom.Shared.Dtos.Users;

namespace StockChatroom.Shared.Dtos.Messages;

public class MessageDto
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserDto? FromUser { get; set; }
}
