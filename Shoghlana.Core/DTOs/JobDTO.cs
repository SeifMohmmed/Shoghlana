using Shoghlana.Core.Enums;
using Shoghlana.Core.Models;

namespace Shoghlana.Core.DTOs;

public class JobDTO
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime PostTime { get; set; } = DateTime.Now;

    public decimal MaxBudget { get; set; }

    public decimal MinBudget { get; set; }

    public ExperienceLevel ExperienceLevel { get; set; }

    // public List<Skill>? Skills { get; set; }

    // public List<Proposal>? Proposals { get; set; }

    //public Rate? Rate { get; set; }

    public Dictionary<int, string> SkillsDic { get; set; } = new Dictionary<int, string>();
    public Dictionary<int, string> FreelancerDic { get; set; } = new Dictionary<int, string>();
    public Dictionary<int, string> ProposalDic { get; set; } = new Dictionary<int, string>();

    public JobStatus JobStatus { get; set; } = JobStatus.Active;


    public int ClientId { get; set; }

    public string? ClientName { get; set; }

    public int CategoryId { get; set; }

    public string? CategoryTitle { get; set; }

    //public List<Freelancer>? AppliedFreelancers { get; set; }

}
