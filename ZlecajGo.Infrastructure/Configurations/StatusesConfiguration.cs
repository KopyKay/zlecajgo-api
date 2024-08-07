using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Infrastructure.Configurations;

internal class StatusesConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        builder.Property(s => s.Name)
            .HasMaxLength(30);
    }
}