using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Infrastructure.Configurations;

public class OfferContractorsConfiguration : IEntityTypeConfiguration<OfferContractor>
{
    public void Configure(EntityTypeBuilder<OfferContractor> builder)
    {
        builder.Property(oc => oc.StatusId)
            .HasDefaultValue(4);
    }
}