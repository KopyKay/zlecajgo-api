namespace ZlecajGo.Infrastructure.Authorization;

public static class PolicyNames
{
    public const string HasProfileCompleted = "HasProfileCompleted";
}

public static class AppClaimTypes
{
    public const string UserName = "UserName";
    public const string FullName = "FullName";
    public const string PhoneNumber = "PhoneNumber";
    public const string BirthDate = "BirthDate";
    public const string IsProfileCompleted = "IsProfileCompleted";
}