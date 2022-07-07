using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockChatroom.Domain.Entities;

namespace StockChatroom.Infrastructure.Data.Mappings;

internal class ChatRoomMappings : IEntityTypeConfiguration<ChatRoom>
{
    public void Configure(EntityTypeBuilder<ChatRoom> b)
    {
        _ = b.HasIndex(x => x.Id);
        _ = b.Property(x => x.Id);
        _ = b.Property(x => x.Name).IsRequired().HasMaxLength(ChatRoom.NameMaxLength);

        _ = b.HasData(new ChatRoom("Main Chat"));
    }
}
