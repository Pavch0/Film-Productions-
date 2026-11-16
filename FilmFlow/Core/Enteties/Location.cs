namespace FilmFlow.Core.Entities;

public class Location
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? ContactPhone { get; set; }
    public string? Notes { get; set; }

    // По-късно: Scenes
}