using ZlecajGo.Domain.Entities;
using ZlecajGo.Infrastructure.Extensions;
using ZlecajGo.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IZlecajGoSeeder>();
await seeder.SeedAsync(false);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();