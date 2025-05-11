namespace Shoghlana.Core.DTOs;

public class FreelancerDTO
{
    public byte[]? PersonalImageBytes { get; set; }

    public string Name { get; set; }

    public string Title { get; set; }

    public string? Address { get; set; }

    public string? OverView { get; set; }


    //public List<Project>? Portfolio { get; set; }

    //public List<Job>? WorkingHistory { get; set; }

    //public List<Proposal>? Proposals { get; set; }

    public List<SkillDTO>? skills { get; set; }

    //public List<Notification>? notifications { get; set; }
}
