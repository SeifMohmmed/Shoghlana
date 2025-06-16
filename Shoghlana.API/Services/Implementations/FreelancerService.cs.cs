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

    public ActionResult<GeneralResponse> GetAll()
    {
        var freelancers = _unitOfWork.freelancerRepository
                                    .FindAll(includes: new[] { "Skills.Skill", "Portfolio", "WorkingHistory" }).ToList();

        List<GetFreelancerDTO> freelancerDTOs = freelancers.Select(freelancer =>
        {
            // Mapping skills
            List<Skill> Skills = new List<Skill>();
            foreach (FreelancerSkills FreelancerSkill in freelancer.Skills)
            {
                Skill? skill = _unitOfWork.skillRepository.GetById(FreelancerSkill.SkillId);
                Skills.Add(skill);
            }
            List<SkillDTO> SkillsDtos = _mapper.Map<List<Skill>, List<SkillDTO>>(Skills);

            // Mapping portfolio projects and their skills
            List<GetProjectDTO> getProjectsDTOs = _mapper.Map<List<Project>, List<GetProjectDTO>>(freelancer.Portfolio);
            for (int i = 0; i < freelancer.Portfolio.Count; i++)
            {
                List<int> ProjectSkillsIds = _unitOfWork.projectSkillsRepository
                                            .FindAll(criteria: ps => ps.ProjectId == freelancer.Portfolio[i].Id)
                                            .Select(ps => ps.SkillId).ToList();

                List<Skill> ProjectSkills = new List<Skill>();
                foreach (int skillId in ProjectSkillsIds)
                {
                    Skill skill = _unitOfWork.skillRepository.GetById(skillId);
                    ProjectSkills.Add(skill);
                }

                List<SkillDTO> skillDTOs = _mapper.Map<List<Skill>, List<SkillDTO>>(ProjectSkills);
                getProjectsDTOs[i].Skills = skillDTOs;
            }

            // Mapping working history and their rates
            List<GetJobDTO> jobDTOs = new List<GetJobDTO>();
            foreach (Job job in freelancer.WorkingHistory)
            {
                GetJobDTO jobDto = _mapper.Map<Job, GetJobDTO>(job);
                Rate? rate = _unitOfWork.rateRepository.Find(r => r.JobId == job.Id);
                RateDTO rateDto = _mapper.Map<Rate, RateDTO>(rate);
                jobDto.Rate = rateDto;
                Category category = _unitOfWork.categoryRepository.GetById(job.CategoryId);
                jobDto.CategoryTitle = category.Title;
                jobDTOs.Add(jobDto);
            }

            // Mapping freelancer details
            return new GetFreelancerDTO
            {
                Id = freelancer.Id,
                Name = freelancer.Name,
                Title = freelancer.Title,
                Address = freelancer.Address,
                OverView = freelancer.Overview,
                PersonalImageBytes = freelancer.PersonalImageBytes,
                Skills = SkillsDtos,
                Portfolio = getProjectsDTOs,
                WorkingHistory = jobDTOs
            };
        }).ToList();

        return new GeneralResponse()
        {
            IsSuccess = true,
            Status = 200,
            Data = freelancerDTOs
        };
    }


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

        var jobDTOs = new List<GetJobDTO>();

        foreach (var job in freelancer.WorkingHistory)
        {
            var jobDTO = _mapper.Map<Job, GetJobDTO>(job);

            var rate = _unitOfWork.rateRepository.Find(r => r.JobId == job.Id);

            var rateDTO = _mapper.Map<Rate, RateDTO>(rate);

            //jobDTO.Rate = rateDTO;

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


    public async Task<ActionResult<GeneralResponse>> AddAsync(AddFreelancerDTO addFreelancerDTO)
    {
        if (addFreelancerDTO.PersonalImageBytes is not null)
        {
            // Validation for personal image
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
        }

        var freelancer = new Freelancer()
        {
            Name = addFreelancerDTO.Name,
            Title = addFreelancerDTO.Title,
            Address = addFreelancerDTO.Address,
            Overview = addFreelancerDTO.Overview,
        };

        if (addFreelancerDTO.PersonalImageBytes is not null)
        {
            using var dataStream = new MemoryStream();

            await addFreelancerDTO.PersonalImageBytes.CopyToAsync(dataStream);

            freelancer.PersonalImageBytes = dataStream.ToArray();
        }


        await _unitOfWork.freelancerRepository.AddAsync(freelancer);

        _unitOfWork.SaveAsync(); // Ensure the freelancer gets an ID

        var skills = (await _unitOfWork.skillRepository.FindAllAsync(criteria: s => addFreelancerDTO.SkillIDs.Contains(s.Id))).ToList();

        var freelancerSkills = new List<FreelancerSkills>();

        foreach (var skill in skills)
        {
            var freelancerSkill = new FreelancerSkills()
            {
                SkillId = skill.Id,
                FreelancerId = freelancer.Id // Assuming freelancer is already retrieved or available
            };

            freelancerSkills.Add(freelancerSkill);
        }

        // Assuming freelancer is already retrieved or available
        freelancer.Skills = freelancerSkills;

        _unitOfWork.freelancerRepository.Update(freelancer);
        await _unitOfWork.SaveAsync();

        //FreelancerDTO freelancerDTO = mapper.Map<Freelancer, FreelancerDTO>(freelancer);

        return new GeneralResponse
        {
            IsSuccess = true,
            Status = 201,
            Data = null,
            Message = "Added Successfully"
        };
    }


    public async Task<ActionResult<GeneralResponse>> UpdateAsync(int id, [FromForm] AddFreelancerDTO updatedFreelancerDTO)
    {
        var freelancer = await _unitOfWork.freelancerRepository.GetByIdAsync(id);

        if (freelancer is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "There is no Freelancer found with this ID !"
            };
        }

        if (updatedFreelancerDTO.PersonalImageBytes != null)
        {
            // Validation for personal image
            if (!allowedExtensions.Contains(Path.GetExtension(updatedFreelancerDTO.PersonalImageBytes.FileName).ToLower()))
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Status = 400,
                    Message = "The allowed Personal Image Extensions are: jpg, png",
                };
            }

            if (updatedFreelancerDTO.PersonalImageBytes.Length > maxAllowedPersonalImageSize)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Status = 400,
                    Message = "The max allowed Personal Image size is 1 MB",
                };
            }

            using var dataStream = new MemoryStream();

            await updatedFreelancerDTO.PersonalImageBytes.CopyToAsync(dataStream);

            freelancer.PersonalImageBytes = dataStream.ToArray();
        }

        freelancer.Name = updatedFreelancerDTO.Name;
        freelancer.Title = updatedFreelancerDTO.Title;
        freelancer.Overview = updatedFreelancerDTO.Overview;
        freelancer.Address = updatedFreelancerDTO.Address;

        if (updatedFreelancerDTO.SkillIDs != null && updatedFreelancerDTO.SkillIDs.Any())
        {
            var oldFreelancerSkills = await _unitOfWork.freelancerSkillsRepository.FindAllAsync(criteria: fs => fs.FreelancerId == freelancer.Id);
            _unitOfWork.freelancerSkillsRepository.DeleteRange(oldFreelancerSkills);

            var skills = (await _unitOfWork.skillRepository.FindAllAsync(criteria: s => updatedFreelancerDTO.SkillIDs.Contains(s.Id))).ToList();

            var newFreelancerSkills = new List<FreelancerSkills>(skills.Count);

            foreach (var skill in skills)
            {
                var freelancerSkill = new FreelancerSkills()
                {
                    SkillId = skill.Id,
                    FreelancerId = freelancer.Id
                };

                newFreelancerSkills.Add(freelancerSkill);
            }

            freelancer.Skills = newFreelancerSkills;
        }

        _unitOfWork.Save();

        return new GeneralResponse()
        {
            IsSuccess = true,
            Status = 200,
            Message = "Freelancer updated successfully"
        };
    }


    public ActionResult<GeneralResponse> Delete(int id)
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
