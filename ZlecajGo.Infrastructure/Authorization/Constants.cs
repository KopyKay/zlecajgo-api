namespace ZlecajGo.Infrastructure.Authorization;

public static class PolicyNames
{
    public const string HasProfileCompleted = "HasProfileCompleted";
}

public static class AppClaimTypes
{
    public const string FullName = "FullName";
    public const string UserName = "UserName";
    public const string IsProfileCompleted = "IsProfileCompleted";
}