namespace ZlecajGo.Domain.Constants;

public static class SqlDefaults
{
    public const string CurrentUtcTimestamp = "CURRENT_TIMESTAMP AT TIME ZONE 'UTC'";
    public const string CurrentUtcTimestampPlus2Days = "CURRENT_TIMESTAMP AT TIME ZONE 'UTC' + INTERVAL '2 days'";
}