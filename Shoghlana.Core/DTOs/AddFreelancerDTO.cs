using Microsoft.AspNetCore.Http;

namespace Shoghlana.Core.DTOs;

public class AddFreelancerDTO
{
    public IFormFile? PersonalImageBytes { get; set; }

    public string Name { get; set; }

    public string? Title { get; set; }

    public string? Address { get; set; }

    public string? Overview { get; set; }


    //public List<Project>? Portfolio { get; set; }

    //public List<Job>? WorkingHistory { get; set; }

    //public List<Proposal>? Proposals { get; set; }

    //public List<Skill>? skills { get; set; }

    //public List<Notification>? notifications { get; set; }
}
