using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoghlana.Core.DTOs;
using Shoghlana.API.Response;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;
using Shoghlana.EF.Repositories;

namespace Shoghlana.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class JobController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public JobController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<GeneralResponse> GetAll()
    {
        var jobs = _unitOfWork.job.FindAll(new string[] { "Category", "Client", "Skills" }).ToList();

        var jobDTOs = _mapper.Map<List<Job>, List<JobDTO>>(jobs);

        for (int i = 0; i < jobs.Count; i++)
        {
            jobDTOs[i].ClientName = jobs[i].Client.Name;
            jobDTOs[i].CategoryTitle = jobs[i].Category.Title;

            foreach (JobSkills jobskill in jobs[i].Skills)
            {
                var skill = _unitOfWork.skill.GetById(jobskill.SkillId);
                jobDTOs[i].skillsDTO.Add(new SkillDTO
                {
                    Title = skill.Title,
                    Id = skill.Id,
                });
            }
        }

        return new GeneralResponse
        {
            IsSuccess = true,
            Data = jobDTOs,
            Message = "All jobs retrieved successfully"
        };

    }
    [HttpGet("{id:int}")]
    public ActionResult<GeneralResponse> GetById(int id)
    {
        var job = new Job();

        var jobDTOs = new JobDTO();

        try
        {
            job = _unitOfWork.job.Find(includes: new string[] { "Skills", "Category", "Proposals", "Client" }, criteria: j => j.Id == id);

            jobDTOs = _mapper.Map<Job, JobDTO>(job);
            jobDTOs.ClientName = job.Client.Name;
            jobDTOs.CategoryTitle = job.Category.Title;

            foreach (var proposal in job.Proposals)
            {
                var freelancer = _unitOfWork.freelancer.GetById(proposal.FreelancerId);
                jobDTOs.freelancersDTO.Add(new FreelancerDTO
                {
                    Name = freelancer.Name,
                    Id = freelancer.Id
                });

                jobDTOs.proposalsDTO.Add(new ProposalDTO
                {
                    Description = proposal.Description,
                    Id = proposal.Id
                });
            }
            foreach (var jobskill in job.Skills)
            {
                Skill skill = _unitOfWork.skill.GetById(jobskill.SkillId);

                jobDTOs.skillsDTO.Add(new SkillDTO
                {
                    Title = skill.Title,
                    Id = skill.Id
                });
            }
        }
        catch (Exception ex)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Data = null,
                Message = ex.Message
            };
        }
        return new GeneralResponse
        {
            IsSuccess = true,
            Data = jobDTOs,
            Message = "Job is retrieved successfully"
        };
    }

    [HttpGet("freelancer")]
    public ActionResult<GeneralResponse> GetByFreelancerId([FromQuery] int id)
    {
        List<Job> jobs;

        try
        {
            jobs = _unitOfWork.job.FindAll(new string[] { "Client", "Category", "Skills" }, j => j.FreelancerId == id)
                                                    .ToList();
        }
        catch (Exception ex)
        {
            return new GeneralResponse
            {
                IsSuccess = false,
                Data = null,
                Message = ex.Message
            };
        }


        List<JobDTO> jobDTOs = _mapper.Map<List<Job>, List<JobDTO>>(jobs);

        for (int i = 0; i < jobs.Count; i++)
        {
            jobDTOs[i].ClientName = jobs[i].Client.Name;
            jobDTOs[i].CategoryTitle = jobs[i].Category.Title;

            foreach (JobSkills jobSkill in jobs[i].Skills)
            {
                Skill skill = _unitOfWork.skill.GetById(jobSkill.SkillId);
                jobDTOs[i].skillsDTO.Add(new SkillDTO
                {
                    Title = skill.Title,
                    Id = skill.Id,
                });
            }
        }
        return new GeneralResponse
        {
            IsSuccess = true,
            Data = jobDTOs,
            Message = "All jobs for this freelancer retrieved successfully"
        };
        // rate?????
    }



    [HttpGet("client")]
    public ActionResult<GeneralResponse> GetByClientId([FromQuery] int id)
    {
        List<Job> jobs;
        try
        {
            jobs = _unitOfWork.job.FindAll(new string[] { "Freelancer", "Category", "Skills" }, j => j.ClientId == id)
                                    .ToList();
        }
        catch (Exception ex)
        {
            return new GeneralResponse
            {
                IsSuccess = false,
                Data = null,
                Message = ex.Message
            };
        }


        List<JobDTO> jobDTOs = _mapper.Map<List<Job>, List<JobDTO>>(jobs);


        for (int i = 0; i < jobs.Count; i++)
        {
            jobDTOs[i].AcceptedFreelancerName = jobs[i].Freelancer.Name;
            jobDTOs[i].AcceptedFreelancerId = jobs[i].Freelancer.Id;
            jobDTOs[i].CategoryTitle = jobs[i].Category.Title;

            foreach (JobSkills jobSkill in jobs[i].Skills)
            {
                Skill skill = _unitOfWork.skill.GetById(jobSkill.SkillId);
                jobDTOs[i].skillsDTO.Add(new SkillDTO
                {
                    Title = skill.Title,
                    Id = skill.Id,
                });
            }
        }
        return new GeneralResponse
        {
            IsSuccess = true,
            Data = jobDTOs,
            Message = "All jobs for this client retrieved successfully"
        };

        // rate?????
    }

    [HttpPost]
    public ActionResult<GeneralResponse> Add(JobDTO jobDTO)
    {
        var job = _mapper.Map<JobDTO, Job>(jobDTO);
        try
        {
            _unitOfWork.job.Add(job);
            _unitOfWork.Save();

            foreach (SkillDTO skillDTO in jobDTO.skillsDTO)
            {
                job.Skills.Add(new JobSkills
                {
                    SkillId = skillDTO.Id,
                    JobId = job.Id
                });
            }
            _unitOfWork.job.Update(job);
            _unitOfWork.Save();
        }

        catch (Exception ex)
        {
            return new GeneralResponse
            {
                IsSuccess = false,
                Data = null,
                Message = ex.Message
            };
        }
        return new GeneralResponse()
        {
            IsSuccess = true,
            Data = jobDTO,
            Message = "Job is added successfully"
        };
    }
    [HttpPut]
    public ActionResult<GeneralResponse> update(JobDTO jobDto)
    {
        Job job = _unitOfWork.job.GetById(jobDto.Id);
        //  job = mapper.Map<JobDTO, Job>(jobDto);
        job.Description = jobDto.Description;
        job.Title = jobDto.Title;
        job.MaxBudget = jobDto.MaxBudget;
        job.MinBudget = jobDto.MinBudget;
        job.ExperienceLevel = jobDto.ExperienceLevel;
        job.CategoryId = jobDto.CategoryId;

        List<JobSkills> jobSkills = _unitOfWork.jobSkills
                               .FindAll(criteria: js => js.JobId == jobDto.Id)
                               .ToList();
        _unitOfWork.jobSkills.DeleteRange(jobSkills);

        List<JobSkills> skills = new List<JobSkills>();
        foreach (SkillDTO skillDto in jobDto.skillsDTO)
        {
            skills.Add(new JobSkills
            {
                SkillId = skillDto.Id,
                JobId = jobDto.Id
            });
        }
        job.Skills = skills;


        try
        {
            _unitOfWork.job.Update(job);
            _unitOfWork.Save();
            return new GeneralResponse
            {
                IsSuccess = true,
                Data = jobDto,
                Message = "Job updated successfully"
            };
        }
        catch (Exception ex)
        {
            return new GeneralResponse
            {
                IsSuccess = false,
                Data = null,
                Message = ex.Message
            };
        }
    }



    [HttpDelete("{id:int}")]
    public ActionResult<GeneralResponse> delete(int id)
    {
        Job job = _unitOfWork.job.GetById(id);
        List<JobSkills> jobSkills = _unitOfWork.jobSkills
                                   .FindAll(criteria: js => js.JobId == id)
                                   .ToList();

        if (jobSkills.Count > 0)
        {
            _unitOfWork.jobSkills.DeleteRange(jobSkills);
        }


        List<Proposal> proposals = _unitOfWork.proposal
                                  .FindAll(criteria: p => p.JobId == id)
                                  .ToList();

        if (proposals.Count > 0)
        {
            foreach (Proposal proposal in proposals)
            {
                List<ProposalImages> images = _unitOfWork.ProposalImages
                                          .FindAll(criteria: pi => pi.ProposalId == proposal.Id)
                                          .ToList();

                _unitOfWork.ProposalImages.DeleteRange(images);
            }

            _unitOfWork.proposal.DeleteRange(proposals);
        }


        Rate rate = _unitOfWork.rate.Find(criteria: r => r.JobId == id);
        if (rate != null)
        {
            _unitOfWork.rate.Delete(rate);
        }

        try
        {
            _unitOfWork.job.Delete(job);
            _unitOfWork.Save();
            return new GeneralResponse
            {
                IsSuccess = true,
                Data = null,
                Message = "Job was deleted successfully"
            };
        }
        catch (Exception ex)
        {
            return new GeneralResponse
            {
                IsSuccess = false,
                Data = null,
                Message = ex.Message
            };
        }
    }
}
