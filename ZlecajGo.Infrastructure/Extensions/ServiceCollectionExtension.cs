using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Repositories;
using ZlecajGo.Infrastructure.Persistence;
using ZlecajGo.Infrastructure.Repositories;
using ZlecajGo.Infrastructure.Seeders;

namespace ZlecajGo.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ZlecajGoDb");

        services.AddDbContext<ZlecajGoContext>(options =>
            options.UseNpgsql(connectionString)
                .EnableSensitiveDataLogging());
        
        services.AddIdentityApiEndpoints<User>()
            .AddEntityFrameworkStores<ZlecajGoContext>();
        
        services.AddScoped<IZlecajGoSeeder, ZlecajGoSeeder>();
        services.AddScoped<IOfferRepository, OfferRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IStatusRepository, StatusRepository>();
        services.AddScoped<ITypeRepository, TypeRepository>();
    }
}