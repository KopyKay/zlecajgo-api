using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Infrastructure.Configurations;

public class UsersConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(user => user.FullName)
            .HasMaxLength(100);
        
        builder.Property(u => u.PhoneNumber)
            .HasMaxLength(12);
        
        builder.Property(u => u.UserName)
            .HasMaxLength(30);
        
        builder.Property(u => u.NormalizedUserName)
            .HasMaxLength(30);
    }
}