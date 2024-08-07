using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Infrastructure.Configurations;

internal class ReviewsConfiguration : IEntityTypeConfiguration<Review>
{
    private const string GetUtcDateSql = "CURRENT_TIMESTAMP AT TIME ZONE 'UTC'";
    
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(r => new { r.ReviewerId, r.RevieweeId });
        
        builder.Property(r => r.Rating)
            .HasMaxLength(1);
        
        builder.Property(r => r.Comment)
            .HasMaxLength(500);
        
        builder.Property(r => r.PostDateTime)
            .HasDefaultValueSql(GetUtcDateSql);

        builder.Property(r => r.UpdateDateTime)
            .ValueGeneratedOnUpdate()
            .HasDefaultValueSql(GetUtcDateSql);

        builder.HasOne(r => r.Reviewer)
            .WithMany(u => u.ReviewsGiven)
            .HasForeignKey(r => r.ReviewerId);

        builder.HasOne(r => r.Reviewee)
            .WithMany(u => u.ReviewsReceived)
            .HasForeignKey(r => r.RevieweeId);
    }
}