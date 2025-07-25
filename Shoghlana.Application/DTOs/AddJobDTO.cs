using Shoghlana.Domain.Enums;

namespace Shoghlana.Application.DTOs;

public class AddJobDTO
{
    public int? Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime? PostTime { get; set; } = DateTime.Now;

    public decimal MaxBudget { get; set; }

    public decimal MinBudget { get; set; }

    public int DurationInDays { get; set; }

    public DateTime? DeadLine { get; set; }

    public ExperienceLevel ExperienceLevel { get; set; }

    public List<int>? SkillsIds { get; set; } = new List<int>();

    public JobStatus? Status { get; set; } = JobStatus.Active;

    public int ClientId { get; set; }

    public string ClientName { get; set; }

    public int CategoryId { get; set; }

    public string? CategoryTitle { get; set; }

}
