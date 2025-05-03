using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.Core.Interfaces;
public interface IUnitOfWork
{
    ICategoryRepository category { get; }
    IFreelancerRepository freelancer { get; }
    IClientRepository client { get; }
    IJobRepository job { get; }
    IJobSkillsRepository jobSkills { get; }
    IRateRepository rate { get; }
    IProposalRepository proposal { get; }
    ISkillRepository skill { get; }
    IFreelancerSkillsRepository freelancerSkills { get; }
    IProjectRepository project { get; }
    IProjectImagesRepository projectImages{ get; }
    IProjectSkillsRepository projectSkills { get; }

    public int save();
    public void Dispose();

}
