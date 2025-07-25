using System.ComponentModel.DataAnnotations.Schema;

namespace Shoghlana.Domain.Entities;
public class FreelancerSkills
{
    //  public int Id { get; set; }

    //----------------------------

    [ForeignKey("Freelancer")]
    public int FreelancerId { get; set; }

    public Freelancer Freelancer { get; set; }

    [ForeignKey("Skill")]
    public int SkillId { get; set; }

    public Skill Skill { get; set; }
}
