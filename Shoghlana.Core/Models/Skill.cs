using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.Core.Models;
public class Skill
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string? Description { get; set; }

    public List<FreelancerSkills>? Freelancers { get; set; }

    public List<JobSkills>? Jobs { get; set; }

    public List<ProjectSkills>? Projects { get; set; }

}
