using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Infrastructure.Configurations;

internal class CategoriesConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(c => c.Name)
            .HasMaxLength(50);
    }
}