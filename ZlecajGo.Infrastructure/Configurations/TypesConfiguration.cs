using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Type = ZlecajGo.Domain.Entities.Type;

namespace ZlecajGo.Infrastructure.Configurations;

internal class TypesConfiguration : IEntityTypeConfiguration<Type>
{
    public void Configure(EntityTypeBuilder<Type> builder)
    {
        builder.Property(t => t.Name)
            .HasMaxLength(15);
    }
}