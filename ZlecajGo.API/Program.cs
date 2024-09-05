using Serilog;
using ZlecajGo.API.Extensions;
using ZlecajGo.API.Middlewares;
using ZlecajGo.Application.Extensions;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Infrastructure.Extensions;
using ZlecajGo.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.AddPresentation();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

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

app.MapGroup("/api/identity")
    .WithTags("Identity")
    .MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

Log.Information("API started");

app.Run();