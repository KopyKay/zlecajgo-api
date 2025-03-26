using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZlecajGo.Domain.Constants;
using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Infrastructure.Configurations;

internal class MessagesConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasIndex(m => m.ChatId);
        builder.HasIndex(m => m.SentAt);
        builder.HasIndex(m => m.SenderId);
        
        builder.Property(m => m.MessageText)
            .IsRequired();

        builder.Property(m => m.SentAt)
            .HasDefaultValueSql(SqlDefaults.CurrentUtcTimestamp)
            .IsRequired();

        builder.HasOne(m => m.Chat)
            .WithMany(c => c.Messages)
            .HasForeignKey(m => m.ChatId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(m => m.Sender)
            .WithMany(u => u.MessagesSent)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}