using AutoMapper;
using Shoghlana.Core.DTOs;
using Shoghlana.Core.Models;

namespace Shoghlana.API.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Freelancer,FreelancerDTO>().ReverseMap();

        CreateMap<Proposal, GetProposalDTO>().ReverseMap();

        CreateMap<SkillDTO,Skill>().ReverseMap();

        CreateMap<Job, JobDTO>().ReverseMap();
    }
}
