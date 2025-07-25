using Microsoft.AspNetCore.Http;

namespace Shoghlana.Application.DTOs;

public class AddFreelancerDTO
{
    public IFormFile? PersonalImageBytes { get; set; }

    public string Name { get; set; }

    public string? Title { get; set; }

    public string? Address { get; set; }

    public string? Overview { get; set; }

    public List<int>? SkillIDs { get; set; } = new List<int> { };

}
