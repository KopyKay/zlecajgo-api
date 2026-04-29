namespace ZlecajGo.API.IntegrationTests.Helpers.TestData;

public static class IdentityTestData
{
    public static object CreateRegisterRequest(string email, string password) => new { email, password };
    public static object CreateLoginRequest(string email, string password) => new { email, password };
}