namespace FilmFlow.Core.Entities;

public class Actor
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }

    // Later: SceneActors
}
