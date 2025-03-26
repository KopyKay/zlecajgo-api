using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZlecajGo.Domain.Constants;
using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Infrastructure.Configurations;

internal class ChatsConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.HasIndex(c => c.User1Id);
        builder.HasIndex(c => c.User2Id);
        builder.HasIndex(c => c.LastMessageAt);

        builder.ToTable(t => t.HasCheckConstraint(
            "CK_Chat_UserOrder",
            "\"User1Id\" < \"User2Id\""));
        
        builder.Property(c => c.CreatedAt)
            .HasDefaultValueSql(SqlDefaults.CurrentUtcTimestamp)
            .IsRequired();
        
        builder.Property(c => c.LastMessageAt)
            .HasDefaultValueSql(SqlDefaults.CurrentUtcTimestamp);

        builder.HasOne(c => c.User1)
            .WithMany(u => u.ChatsAsUser1)
            .HasForeignKey(c => c.User1Id)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(c => c.User2)
            .WithMany(u => u.ChatsAsUser2)
            .HasForeignKey(c => c.User2Id)
            .OnDelete(DeleteBehavior.NoAction);
    }
}