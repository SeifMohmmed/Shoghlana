using AutoMapper;
using Shoghlana.Core.DTOs;
using Shoghlana.Core.Models;

namespace Shoghlana.API.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Freelancer, FreelancerDTO>().ReverseMap();

        CreateMap<Proposal, GetProposalDTO>().ReverseMap();

        CreateMap<SkillDTO, Skill>().ReverseMap();

        CreateMap<JobSkills, SkillDTO>().ReverseMap();

        CreateMap<Project, GetProjectDTO>().ReverseMap();

        CreateMap<ProjectImages, GetImageDTO>().ReverseMap();

        CreateMap<Job, JobDTO>().ReverseMap();

        CreateMap<Proposal, GetProposalDTO>().ReverseMap();

        CreateMap<Proposal, GetPropsalImageDTO>().ReverseMap();

        CreateMap<Proposal, ProposalDTO>().ReverseMap();

        CreateMap<Rate, RateDTO>().ReverseMap();

        CreateMap<Category, CategoryDTO>().ReverseMap();

        CreateMap<Category, GetTitleOfCategoryDTO>().ReverseMap();
    }
}
