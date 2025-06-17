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

        CreateMap<Project, GetProjectDTO>()
            .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills.Select(ps => ps.Skill)));

        CreateMap<GetProjectDTO, Project>()
            .ForMember(dest => dest.Skills, opt => opt.Ignore());

        CreateMap<Project, AddProjectDTO>();

        CreateMap<AddProjectDTO, Project>();

        CreateMap<ProjectImages, GetImageDTO>().ReverseMap();

        CreateMap<Job, AddJobDTO>().ReverseMap();

        CreateMap<GetJobDTO, Job>().ReverseMap();

        CreateMap<Proposal, GetProposalDTO>().ReverseMap();

        CreateMap<Proposal, GetPropsalImageDTO>().ReverseMap();

        CreateMap<Proposal, ProposalDTO>().ReverseMap();

        CreateMap<Rate, RateDTO>().ReverseMap();

        CreateMap<Category, CategoryDTO>().ReverseMap();

        CreateMap<Category, GetTitleOfCategoryDTO>().ReverseMap();

        //CreateMap<FreelancerNotification, GetFreelancerNotificationDTO>().ReverseMap();

        //CreateMap<GetNotificationDTO, GetClientNotificationDTO>().ReverseMap();

        CreateMap<Notification,GetNotificationDTO>().ReverseMap();

    }
}
