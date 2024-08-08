using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Infrastructure.Persistence;
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
        
        services.AddIdentityCore<User>()
            .AddEntityFrameworkStores<ZlecajGoContext>();
        
        services.AddScoped<IZlecajGoSeeder, ZlecajGoSeeder>();
    }
}