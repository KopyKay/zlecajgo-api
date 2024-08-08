using Serilog;
using ZlecajGo.API.Extensions;
using ZlecajGo.API.Middlewares;
using ZlecajGo.Infrastructure.Extensions;
using ZlecajGo.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.AddPresentation();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IZlecajGoSeeder>();
await seeder.SeedAsync(false);

app.UseSerilogRequestLogging();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();