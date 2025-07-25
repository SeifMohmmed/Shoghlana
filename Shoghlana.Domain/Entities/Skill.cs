namespace Shoghlana.Domain.Entities;
public class Skill
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string? Description { get; set; }

    public List<FreelancerSkills>? Freelancers { get; set; }

    public List<JobSkills>? Jobs { get; set; }

    public List<ProjectSkills>? Projects { get; set; }

}
