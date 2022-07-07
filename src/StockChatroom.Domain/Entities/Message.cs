namespace StockChatroom.Domain.Entities;

public class Message
{
    public const int TextMaxLength = 250;

    public Guid Id { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid ChatRoomId { get; set; }
    public virtual ChatRoom ChatRoom { get; set; }
    public string FromUserId { get; set; }
    public virtual ApplicationUser FromUser { get; set; }

    public Message(string text, ChatRoom chatRoom, ApplicationUser fromUser)
    {
        Id = Guid.NewGuid();
        Text = text;
        CreatedAt = DateTime.Now;
        ChatRoom = chatRoom;
        FromUser = fromUser;
    }

    private Message()
    {
    }
}
