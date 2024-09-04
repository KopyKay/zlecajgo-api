using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZlecajGo.Domain.Entities;
using Type = ZlecajGo.Domain.Entities.Type;

namespace ZlecajGo.Infrastructure.Persistence;

internal class ZlecajGoContext(DbContextOptions<ZlecajGoContext> options) : IdentityDbContext<User>(options)
{
    internal DbSet<Category> Categories { get; set; }
    internal DbSet<OfferContractor> OfferContractors { get; set; }
    internal DbSet<Offer> Offers { get; set; }
    internal DbSet<Review> Reviews { get; set; }
    internal DbSet<Status> Statuses { get; set; }
    internal DbSet<Type> Types { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ZlecajGoContext).Assembly);
    }
}