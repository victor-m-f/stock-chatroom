using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockChatroom.Domain.Entities;

namespace StockChatroom.Infrastructure.Data.Mappings;

internal class MessageMappings : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> b)
    {
        _ = b.HasIndex(x => x.Id);
        _ = b.Property(x => x.Id);
        _ = b.Property(x => x.Text).IsRequired().HasMaxLength(Message.TextMaxLength);
    }
}
