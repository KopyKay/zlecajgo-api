namespace ZlecajGo.Infrastructure.Seeders;

public interface IZlecajGoSeeder
{
    Task SeedAsync(bool enabled = true);
}