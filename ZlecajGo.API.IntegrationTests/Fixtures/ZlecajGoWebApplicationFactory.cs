using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ZlecajGo.Infrastructure.Persistence;
using ZlecajGo.Infrastructure.Seeders;

namespace ZlecajGo.API.IntegrationTests.Fixtures;

public class ZlecajGoWebApplicationFactory(string databaseName) : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<ZlecajGoContext>));
            services.AddDbContext<ZlecajGoContext>(options =>
                options.UseInMemoryDatabase(databaseName));

            services.RemoveAll(typeof(IZlecajGoSeeder));
            services.AddScoped<IZlecajGoSeeder, TestDictionarySeeder>();
        });
    }
}