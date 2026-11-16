using FilmFlow.Core.Enums;

namespace FilmFlow.Core.Entities;

public class ProductionMember
{
    public int ProductionId { get; set; }
    public Production Production { get; set; } = null!;

    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;

    public CrewRole CrewRole { get; set; } = CrewRole.Other;
}