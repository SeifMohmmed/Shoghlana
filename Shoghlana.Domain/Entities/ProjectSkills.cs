using System.ComponentModel.DataAnnotations.Schema;

namespace Shoghlana.Domain.Entities;
public class ProjectSkills
{
    // public int Id { get; set; }

    //----------------------------

    [ForeignKey("Project")]
    public int ProjectId { get; set; }

    public Project Project { get; set; }

    [ForeignKey("Skill")]
    public int SkillId { get; set; }

    public Skill Skill { get; set; }

}
