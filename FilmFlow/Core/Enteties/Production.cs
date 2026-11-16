using FilmFlow.Core.Enums;

namespace FilmFlow.Core.Entities;

public class Production
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ProductionStatus Status { get; set; } = ProductionStatus.Planning;
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public decimal Budget { get; set; }

    public string ProducerId { get; set; } = string.Empty;
    public ApplicationUser Producer { get; set; } = null!;

    public ICollection<ProductionMember> Members { get; set; } = new List<ProductionMember>();

    // По-късно: Scenes, ShootingDays, Tasks
}