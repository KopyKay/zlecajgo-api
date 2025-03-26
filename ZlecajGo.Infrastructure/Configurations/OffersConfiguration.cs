using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZlecajGo.Domain.Constants;
using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Infrastructure.Configurations;

internal class OffersConfiguration : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.Property(o => o.Title)
            .HasMaxLength(70);
        
        builder.Property(o => o.Description)
            .HasMaxLength(500);

        builder.Property(o => o.Price)
            .HasPrecision(7, 2);
        
        builder.Property(o => o.PostDateTime)
            .HasDefaultValueSql(SqlDefaults.CurrentUtcTimestamp);

        builder.Property(o => o.ExpiryDateTime)
            .HasDefaultValueSql(SqlDefaults.CurrentUtcTimestampPlus2Days);
        
        builder.OwnsOne(o => o.Location, location =>
        {
            location.Property(l => l.City)
                .HasMaxLength(40);
            
            location.Property(l => l.Street)
                .HasMaxLength(60);
            
            location.Property(l => l.ZipCode)
                .HasMaxLength(6);
        });

        builder.HasOne(o => o.Category)
            .WithMany(c => c.Offers)
            .HasForeignKey(o => o.CategoryId);
        
        builder.HasOne(o => o.Status)
            .WithMany(s => s.Offers)
            .HasForeignKey(o => o.StatusId);
        
        builder.Property(o => o.StatusId)
            .HasDefaultValue(1);
        
        builder.HasOne(o => o.Type)
            .WithMany(t => t.Offers)
            .HasForeignKey(o => o.TypeId);
        
        builder.HasOne(o => o.Provider)
            .WithMany(u => u.ProvidedOffers)
            .HasForeignKey(o => o.ProviderId);
        
        builder.HasMany(o => o.Contractors)
            .WithMany(u => u.ContractedOffers)
            .UsingEntity<OfferContractor>(
                etb => etb.HasOne(co => co.Contractor)
                    .WithMany()
                    .HasForeignKey(co => co.ContractorId),
                
                etb => etb.HasOne(co => co.Offer)
                    .WithMany()
                    .HasForeignKey(co => co.OfferId),

                et =>
                {
                    et.HasKey(co => new { co.ContractorId, co.OfferId });
                }
            );
    }
}