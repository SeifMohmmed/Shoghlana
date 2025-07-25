using Shoghlana.Domain.Enums;

namespace Shoghlana.Application.DTOs;
public class GetJobDTO
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime PostTime { get; set; } = DateTime.Now;

    public decimal MaxBudget { get; set; }

    public decimal MinBudget { get; set; }

    public ExperienceLevel ExperienceLevel { get; set; }

    public List<SkillDTO>? Skills { get; set; } = new List<SkillDTO>();

    public List<GetProposalDTO>? Proposals { get; set; } = new List<GetProposalDTO>();

    public int ProposalsCount { get; set; }

    public JobStatus Status { get; set; } = JobStatus.Active;

    public int ClientId { get; set; }

    public string ClientName { get; set; }

    public int? AcceptedFreelancerId { get; set; }

    public string? AcceptedFreelancerName { get; set; }

    public int CategoryId { get; set; }

    public string? CategoryTitle { get; set; }

    public RateDTO? Rate { get; set; }

}
