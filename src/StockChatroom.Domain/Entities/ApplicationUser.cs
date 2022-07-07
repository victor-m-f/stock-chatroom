using Microsoft.AspNetCore.Identity;

namespace StockChatroom.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public virtual ICollection<Message> Messages { get; set; }

    public ApplicationUser()
    {
        Messages = new HashSet<Message>();
    }
}
