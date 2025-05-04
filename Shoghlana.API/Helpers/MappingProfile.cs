using AutoMapper;
using Shoghlana.API.DTOs;
using Shoghlana.Core.Models;

namespace Shoghlana.API.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Freelancer,FreelancerDTO>().ReverseMap();
    }
}
