namespace FilmFlow.Core.Constants;

public static class AppRoles
{
    public const string Administrator = "Administrator";
    public const string Producer = "Producer";
    public const string CrewMember = "CrewMember";

    public static readonly string[] All = { Administrator, Producer, CrewMember };
}