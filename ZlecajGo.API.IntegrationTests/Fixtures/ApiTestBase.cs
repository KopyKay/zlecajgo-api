using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ZlecajGo.Domain.Entities;

namespace ZlecajGo.API.IntegrationTests.Fixtures;

public abstract class ApiTestBase : IAsyncLifetime
{
    private readonly string _databaseName = $"ZlecajGoTest_{Guid.NewGuid()}";

    protected ZlecajGoWebApplicationFactory Factory { get; private set; } = null!;
    protected HttpClient Client { get; private set; } = null!;

    public Task InitializeAsync()
    {
        Factory = new ZlecajGoWebApplicationFactory(_databaseName);
        Client = Factory.CreateClient();
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        Client.Dispose();
        Factory.Dispose();
        return Task.CompletedTask;
    }

    protected async Task SetProfileCompletedAsync(string email, bool isCompleted = true)
    {
        using var scope = Factory.Services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var user = await userManager.FindByEmailAsync(email);

        if (user == null)
        {
            throw new InvalidOperationException($"User with email '{email}' was not found.");
        }

        user.IsProfileCompleted = isCompleted;
        await userManager.UpdateAsync(user);
    }
}