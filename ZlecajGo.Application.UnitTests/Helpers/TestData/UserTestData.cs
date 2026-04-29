using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Application.UnitTests.Helpers.TestData;

public static class UserTestData
{
    public static User Create(string? id = null) => new()
    {
        Id = id ?? Guid.NewGuid().ToString(),
        UserName = "user",
        Email = "user@test.local"
    };
}