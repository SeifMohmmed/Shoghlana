using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shoghlana.API.Response;
using Shoghlana.API.Services.Interfaces;
using Shoghlana.Core.DTOs;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;

namespace Shoghlana.API.Services.Implementations;

public class FreelancerService : GenericService<Freelancer>, IFreelancerService
{
    private readonly IMapper _mapper;
    private List<string> allowedExtensions = new List<string>() { ".jpg", ".png" };
    private long maxAllowedPersonalImageSize = 1_048_576;  // 1 MB = 1024 * 1024 bytes

    public FreelancerService(IUnitOfWork unitOfWork, IGenericRepository<Freelancer> repository, IMapper mapper)
        : base(unitOfWork, repository)
    {
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<GeneralResponse> GetAll()
    {
        var freelancers = _unitOfWork.freelancerRepository
                          .FindAll(includes: new[] { "Skills" }).ToList();

        var freelancerDTOs = new List<FreelancerDTO>(freelancers.Count());

        foreach (var freelancer in freelancers)
        {
            var freelancerDTO = _mapper.Map<Freelancer, FreelancerDTO>(freelancer);

            freelancerDTOs.Add(freelancerDTO);
        }
        return new GeneralResponse()
        {
            IsSuccess = true,
            Status = 200,
            Data = freelancerDTOs
        };
    }


    [HttpGet("{id:int}")]
    public ActionResult<GeneralResponse> GetById(int id)
    {
        var freelancer = _unitOfWork.freelancerRepository
                         .Find(criteria: f => f.Id == id, includes: new[] { "Skills", "Portfolio", "WorkingHistory" });

        if (freelancer is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "There is no Freelancer found with this ID !"

            };
        }

        //var freelancerDTO = _mapper.Map<Freelancer, FreelancerDTO>(freelancer);

        var GetFreelancerDTO = new GetFreelancerDTO()
        {
            Id = freelancer.Id,
            Name = freelancer.Name,
            Address = freelancer.Address,
            OverView = freelancer.Overview,
            PersonalImageBytes = freelancer.PersonalImageBytes,
            Title = freelancer.Title
        };


        var Skills = new List<Skill>();

        foreach (var freelancerSkill in freelancer.Skills)
        {
            var skill = _unitOfWork.skillRepository.GetById(freelancerSkill.SkillId);

            Skills.Add(skill);
        }

        var SkillsDTOs = _mapper.Map<List<Skill>, List<SkillDTO>>(Skills);

        GetFreelancerDTO.Skills = SkillsDTOs;

        var getProjectsDTOs = new List<GetProjectDTO>();

        getProjectsDTOs = _mapper.Map<List<Project>, List<GetProjectDTO>>(freelancer.Portfolio);

        for (int i = 0; i < freelancer.Portfolio.Count; i++)
        {
            var projectSkillsIds = _unitOfWork.projectSkillsRepository
                .FindAll(criteria: ps => ps.ProjectId == freelancer.Portfolio[i].Id)
                .Select(ps => ps.SkillId).ToList();

            var ProjectSkills = new List<Skill>();

            foreach (var skillId in projectSkillsIds)
            {
                var skill = _unitOfWork.skillRepository.GetById(skillId);

                ProjectSkills.Add(skill);
            }

            var skillDTOs = _mapper.Map<List<Skill>, List<SkillDTO>>(ProjectSkills);
            getProjectsDTOs[i].Skills = skillDTOs;
        }
        GetFreelancerDTO.Portfolio = getProjectsDTOs;

        var jobDTOs=new List<JobDTO>();

        foreach(var job in freelancer.WorkingHistory)
        {
            var jobDTO=_mapper.Map<Job,JobDTO>(job);

            var rate = _unitOfWork.rateRepository.Find(r => r.JobId == job.Id);

            var rateDTO = _mapper.Map<Rate,RateDTO>(rate);

            jobDTO.Rate = rateDTO;

            var category = _unitOfWork.categoryRepository.GetById(job.CategoryId);
            jobDTO.CategoryTitle = category.Title;

            jobDTOs.Add(jobDTO);
        }
        GetFreelancerDTO.WorkingHistory = jobDTOs;


        return new GeneralResponse()
        {
            IsSuccess = true,
            Status = 200,
            Data = GetFreelancerDTO
        };
    }


    [HttpPost]
    public async Task<ActionResult<GeneralResponse>> AddAsync(AddFreelancerDTO addFreelancerDTO)
    {
        if (addFreelancerDTO.PersonalImageBytes is not null)
        {
            //return new GeneralResponse()
            //{
            //    IsSuccess = false,
            //    Status = 400,
            //    Message = "Personal Image is required!"
            //};
        }

        if (!allowedExtensions.Contains(Path.GetExtension(addFreelancerDTO.PersonalImageBytes.FileName).ToLower()))
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "The allowed Personal Image Extensions => {jpg , png}",
            };
        }

        if (addFreelancerDTO.PersonalImageBytes.Length > maxAllowedPersonalImageSize)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "The max Allowed Personal Image Size => 1 MB ",
            };
        }

        using var dataStream = new MemoryStream();

        await addFreelancerDTO.PersonalImageBytes.CopyToAsync(dataStream);

        var freelancer = new Freelancer()
        {
            Name = addFreelancerDTO.Name,
            Title = addFreelancerDTO.Title,
            Address = addFreelancerDTO.Address,
            Overview = addFreelancerDTO.Overview,
            PersonalImageBytes = dataStream.ToArray(),
        };

        var addfreelancer = await _unitOfWork.freelancerRepository.AddAsync(freelancer);

        _unitOfWork.Save();

        var freelancerDTO = _mapper.Map<Freelancer, FreelancerDTO>(freelancer);

        return new GeneralResponse()
        {
            IsSuccess = true,
            Status = 201,
            Data = freelancerDTO,
            Message = "Added Successfully"
        };

    }


    [HttpPut("{id:int}")]
    public async Task<ActionResult<GeneralResponse>> UpdateAsync(int id, [FromForm] AddFreelancerDTO addFreelancerDTO)
    {
        var freelancer = _unitOfWork.freelancerRepository.GetById(id);

        if (freelancer is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "There is no Freelancer found with this ID !"
            };
        }

        if (addFreelancerDTO.PersonalImageBytes != null)
        {
            if (!allowedExtensions.Contains(Path.GetExtension(addFreelancerDTO.PersonalImageBytes.FileName).ToLower()))
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Status = 400,
                    Message = "The allowed Personal Image Extensions => {jpg , png}",
                };
            }

            if (addFreelancerDTO.PersonalImageBytes.Length > maxAllowedPersonalImageSize)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Status = 400,
                    Message = "The max Allowed Personal Image Size => 1 MB ",
                };
            }

            using var dataStream = new MemoryStream();

            await addFreelancerDTO.PersonalImageBytes.CopyToAsync(dataStream);

            freelancer.PersonalImageBytes = dataStream.ToArray();

        }
        freelancer.Name = addFreelancerDTO.Name;
        freelancer.Title = addFreelancerDTO.Title;
        freelancer.Overview = addFreelancerDTO.Overview;
        freelancer.Address = addFreelancerDTO.Address;

        _unitOfWork.Save();

        var freelancerDTO = _mapper.Map<Freelancer, FreelancerDTO>(freelancer);

        return new GeneralResponse()
        {
            IsSuccess = true,
            Status = 200,
            Data = freelancerDTO
        };
    }


    [HttpDelete("{id:int}")]
    public ActionResult<GeneralResponse> Delete(int id)
    {
        Freelancer? freelancer = _unitOfWork.freelancerRepository.GetById(id);

        if (freelancer is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "There is no Freelancer found with this ID !"
            };
        }

        _unitOfWork.freelancerRepository.Delete(freelancer);

        _unitOfWork.Save();

        return new GeneralResponse()
        {
            IsSuccess = true,
            Status = 204, // no content
            Message = $"The Freelancer with ID ({freelancer.Id}) is deleted successfully !"
        };
    }

}
