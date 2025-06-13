using AutoMapper;
using Shoghlana.API.Response;
using Shoghlana.API.Services.Interfaces;
using Shoghlana.Core.DTOs;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;
using Shoghlana.EF;
using Shoghlana.EF.Repositories;

namespace Shoghlana.API.Services.Implementations;

public class SkillService : GenericService<Skill>, ISkillService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SkillService(IUnitOfWork unitOfWork, IGenericRepository<Skill> repository, IMapper mapper) 
        : base(unitOfWork, repository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GeneralResponse> GetAllAsync()
    {
        var skills = await _unitOfWork.skillRepository.FindAllAsync();

        if (skills.Count() == 0)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Data=null,
                Message="No Skills Found"
            };
        }
        var skillDTOs = _mapper.Map<List<Skill>, List<SkillDTO>>(skills.ToList());

        return new GeneralResponse()
        {
            IsSuccess = true,
            Data = skillDTOs,
            Message = "Skills were retrieved successfully"
        };
    }

    public async Task<GeneralResponse> GetByIdAsync(int id)
    {
        var skills = await _unitOfWork.skillRepository.FindAsync(s=>s.Id==id);

        if (skills is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Data = null,
                Message = "No Skills Found"
            };
        }
        var skillDTOs = _mapper.Map<Skill,SkillDTO>(skills);

        return new GeneralResponse()
        {
            IsSuccess = true,
            Data = skillDTOs,
            Message = "Skills were retrieved successfully"
        };
    }
}
