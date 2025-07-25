namespace Shoghlana.Application.DTOs;

public class FreelancerDTO
{
    public int Id { get; set; }

    public byte[]? PersonalImageBytes { get; set; }

    public string Name { get; set; }

    public string Title { get; set; }

    public string? Address { get; set; }

    public string? OverView { get; set; }

    public List<AddProjectDTO>? Portfolio { get; set; }

    public List<GetJobDTO>? WorkingHistory { get; set; }

    public List<ProposalDTO>? Proposals { get; set; }

    public List<SkillDTO>? Skills { get; set; }

}
