using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shoghlana.API.Response;
using Shoghlana.API.Services.Interfaces;
using Shoghlana.Core.DTOs;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;

namespace Shoghlana.API.Services.Implementations;

public class JobService : GenericService<Job>, IJobService
{
    private readonly IMapper _mapper;

    public JobService(IUnitOfWork unitOfWork, IGenericRepository<Job> repository, IMapper mapper)
        : base(unitOfWork, repository)
    {
        _mapper = mapper;
    }
    
    [HttpGet]
    public ActionResult<GeneralResponse> Get(int id)
    {
        Job? job = new Job();

        var jobDTOs = new JobDTO();

        try
        {
            job = _unitOfWork.jobRepository.Find(includes: new string[] { "Skills", "Category", "Proposals", "Client" }, criteria: j => j.Id == id);

            jobDTOs = _mapper.Map<Job, JobDTO>(job);
            jobDTOs.ClientName = job.Client.Name;
            jobDTOs.CategoryTitle = job.Category.Title;

            foreach (var proposal in job.Proposals)
            {
                var freelancer = _unitOfWork.freelancerRepository.GetById(proposal.FreelancerId);
                jobDTOs.Freelancers.Add(new FreelancerDTO
                {
                    Name = freelancer.Name,
                    Id = freelancer.Id
                });

                jobDTOs.Proposals.Add(new ProposalDTO
                {
                    Description = proposal.Description,
                    Id = proposal.Id
                });
            }
            foreach (var jobskill in job.Skills)
            {
                Skill? skill = _unitOfWork.skillRepository.GetById(jobskill.SkillId);

                jobDTOs.Skills.Add(new SkillDTO
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


    [HttpGet]
    public ActionResult<GeneralResponse> GetAll()
    {
        var jobs = _unitOfWork.jobRepository.FindAll(new string[] { "Category", "Client", "Skills" }).ToList();

        var jobDTOs = _mapper.Map<List<Job>, List<JobDTO>>(jobs);

        for (int i = 0; i < jobs.Count; i++)
        {
            jobDTOs[i].ClientName = jobs[i].Client.Name;
            jobDTOs[i].CategoryTitle = jobs[i].Category.Title;
            
            var client = 
                _unitOfWork.clientRepository.GetById(jobDTOs[i].ClientId);

            var category =
                _unitOfWork.categoryRepository.GetById(jobDTOs[i].CategoryId);

            var freelancer=
                _unitOfWork.freelancerRepository.GetById(jobDTOs[i].AcceptedFreelancerId);

            jobDTOs[i].ProposalCount = 
                _unitOfWork.proposalRepository.GetCount();

            foreach (JobSkills jobskill in jobs[i].Skills)
            {
                var skill = _unitOfWork.skillRepository.GetById(jobskill.SkillId);
                jobDTOs[i].Skills.Add(new SkillDTO
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
            Message = "All jobs Retrieved Successfully"
        };
    }


    public ActionResult<GeneralResponse> GetPaginatedJobs
        (int MinBudget,int MaxBudget,int CategoryId,int ClientId,int FreelancerId,
        int page, int pageSize, string[]includes = null)
    {
        var paginatedJobs = _unitOfWork.jobRepository.GetPaginatedJobs(MinBudget, MaxBudget, CategoryId, ClientId
                            , FreelancerId, page, pageSize, includes);

        if(paginatedJobs.Items is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Data = new PaginationListDTO<JobDTO>
                {
                    CurrentPage=paginatedJobs.CurrentPage,
                    TotalPages=paginatedJobs.TotalPages,
                    TotalItems=paginatedJobs.TotalItems,
                    Items=null
                },
                Status=200,
                Message= "No Jobs Found with this filteration"
            };
        }

        var jobDTOs= new List<JobDTO>();

        foreach (var job in paginatedJobs.Items)
        {
            var jobDTO=_mapper.Map<Job,JobDTO>(job);

            var client = 
                _unitOfWork.clientRepository.GetById(jobDTO.ClientId);
            jobDTO.ClientName = client?.Name ?? "NA";

            var category =
                _unitOfWork.categoryRepository.GetById(jobDTO.CategoryId);
            jobDTO.CategoryTitle = category?.Title ?? "NA";

            var freelancer =
                _unitOfWork.freelancerRepository.GetById(jobDTO.AcceptedFreelancerId);
            jobDTO.AcceptedFreelancerName = freelancer?.Name ?? "NA";

            jobDTO.ProposalCount=_unitOfWork.proposalRepository.GetCount();

            jobDTOs.Add(jobDTO);
        }

        return new GeneralResponse()
        {
            IsSuccess = true,
            Data = new PaginationListDTO<JobDTO>
            {
                CurrentPage=paginatedJobs.CurrentPage,
                TotalItems=paginatedJobs.TotalItems,
                TotalPages=paginatedJobs.TotalPages,
                Items=jobDTOs
            },
            Status=200,
        };
    }

    public async Task<ActionResult<GeneralResponse>> GetPaginatedJobsAsync
    (int MinBudget, int MaxBudget, int CategoryId, int ClientId, int FreelancerId,
    int page, int pageSize, string[] includes = null)
    {
        var paginatedJobs = await _unitOfWork.jobRepository.GetPaginatedJobsAsync(MinBudget, MaxBudget, CategoryId, ClientId
                            , FreelancerId, page, pageSize, includes);

        return new GeneralResponse()
        {
            IsSuccess = true,
            Data = paginatedJobs,
            Status = 200
        };
    }


    [HttpGet("freelancer")]
    public ActionResult<GeneralResponse> GetByFreelancerId([FromQuery] int id)
    {
        List<Job> jobs;

        try
        {
            jobs = _unitOfWork.jobRepository.FindAll(new string[] { "Client", "Category", "Skills" }, j => j.FreelancerId == id)
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
                Skill? skill = _unitOfWork.skillRepository.GetById(jobSkill.SkillId);
                jobDTOs[i].Skills.Add(new SkillDTO
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


    [HttpGet("category/{id:int}")]
    public ActionResult<GeneralResponse> GetJobsByCategoryId(int id)
    {
        var category = _unitOfWork.categoryRepository.GetCategoryWithJobs(id);

        if (category != null)
        {
            var categoryDTO = _mapper.Map<CategoryDTO>(category);
            return new GeneralResponse()
            {
                IsSuccess = true,
                Status = 200,
                Data = categoryDTO
            };
        }
        return new GeneralResponse()
        {
            IsSuccess = false,
            Status = 400,
            Message = "Category Not Found !"
        };
    }


    [HttpGet("categories")]
    public ActionResult<GeneralResponse> GetJobsByCategoryIds([FromQuery] List<int> ids)
    {
        List<Job> jobs = new List<Job>();
        foreach (int id in ids)
        {
            List<Job> tempJobs = new List<Job>();
            try
            {
                tempJobs = _unitOfWork.jobRepository.FindAll(criteria: j => j.CategoryId == id)
                                                .ToList();
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
            if (tempJobs != null)
            {
                jobs.AddRange(tempJobs);
            }
        }

        if (jobs.Count > 0)
        {
            return new GeneralResponse()
            {
                IsSuccess = true,
                Data = jobs,
                Message = "All messages for this categories retrieved successfully"
            };
        }

        return new GeneralResponse()
        {
            IsSuccess = false,
            Data = null,
            Message = "No jobs found for these Ids"
        };

    }


    [HttpGet("client")]
    public ActionResult<GeneralResponse> GetByClientId([FromQuery] int id)
    {
        List<Job> jobs;
        try
        {
            jobs = _unitOfWork.jobRepository.FindAll(new string[] { "Freelancer", "Category", "Skills" }, j => j.ClientId == id)
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
                Skill? skill = _unitOfWork.skillRepository.GetById(jobSkill.SkillId);
                jobDTOs[i].Skills.Add(new SkillDTO
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
            _unitOfWork.jobRepository.Add(job);
            _unitOfWork.Save();

            foreach (SkillDTO skillDTO in jobDTO.Skills)
            {
                job.Skills.Add(new JobSkills
                {
                    SkillId = skillDTO.Id,
                    JobId = job.Id
                });
            }
            _unitOfWork.jobRepository.Update(job);
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
    public ActionResult<GeneralResponse> Update(JobDTO jobDto)
    {
        Job job = _unitOfWork.jobRepository.GetById(jobDto.Id);
        //  job = mapper.Map<JobDTO, Job>(jobDto);
        job.Description = jobDto.Description;
        job.Title = jobDto.Title;
        job.MaxBudget = jobDto.MaxBudget;
        job.MinBudget = jobDto.MinBudget;
        job.ExperienceLevel = jobDto.ExperienceLevel;
        job.CategoryId = jobDto.CategoryId;

        List<JobSkills> jobSkills = _unitOfWork.jobSkillsRepository
                               .FindAll(criteria: js => js.JobId == jobDto.Id)
                               .ToList();
        _unitOfWork.jobSkillsRepository.DeleteRange(jobSkills);

        List<JobSkills> skills = new List<JobSkills>();
        foreach (SkillDTO skillDto in jobDto.Skills)
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
            _unitOfWork.jobRepository.Update(job);
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

    
    [HttpDelete]
    public ActionResult<GeneralResponse> Delete(int id)
    {
        Job? job = _unitOfWork.jobRepository.GetById(id);
        List<JobSkills> jobSkills = _unitOfWork.jobSkillsRepository
                                   .FindAll(criteria: js => js.JobId == id)
                                   .ToList();

        if (jobSkills.Count > 0)
        {
            _unitOfWork.jobSkillsRepository.DeleteRange(jobSkills);
        }


        List<Proposal> proposals = _unitOfWork.proposalRepository
                                  .FindAll(criteria: p => p.JobId == id)
                                  .ToList();

        if (proposals.Count > 0)
        {
            foreach (Proposal proposal in proposals)
            {
                List<ProposalImages> images = _unitOfWork.proposalImageRepository
                                          .FindAll(criteria: pi => pi.ProposalId == proposal.Id)
                                          .ToList();

                _unitOfWork.proposalImageRepository.DeleteRange(images);
            }

            _unitOfWork.proposalRepository.DeleteRange(proposals);
        }


        Rate? rate = _unitOfWork.rateRepository.Find(criteria: r => r.JobId == id);

        if (rate != null)
        {
            _unitOfWork.rateRepository.Delete(rate);
        }

        try
        {
            _unitOfWork.jobRepository.Delete(job);
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
