namespace StockChatroom.Domain.Entities;

public class ChatRoom
{
    public const int NameMaxLength = 20;

    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Message> Messages { get; set; }

    public ChatRoom(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        Messages = new HashSet<Message>();
    }

    private ChatRoom()
    {
    }
}
