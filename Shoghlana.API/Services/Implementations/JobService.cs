using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shoghlana.API.Response;
using Shoghlana.API.Services.Interfaces;
using Shoghlana.Core.DTOs;
using Shoghlana.Core.Enums;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        Job? job =
            _unitOfWork.jobRepository.Find(criteria: j => j.Id == id, includes: ["Proposals", "Skills", "Category", "Client"]);

        if (job is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Data = null,
                Message = "No Job found with this ID"
            };
        }

        var jobDTO = new GetJobDTO();
        var skillDTOs = new List<SkillDTO>();

        foreach (var skill in skillDTOs)
        {
            var skillDTO = _mapper.Map<SkillDTO>(skill);

            skillDTOs.Add(skillDTO);
        }

        jobDTO.Skills = skillDTOs;

        if (job.Proposals is not null)
        {
            var proposalDTOs = new List<GetProposalDTO>();

            foreach (var proposal in job.Proposals)
            {
                var proposalDTO = _mapper.Map<GetProposalDTO>(proposal);

                proposalDTOs.Add(proposalDTO);
            }

            //jobDTO.Proposals = proposalDTOs;
        }
        //jobDTO.ClientName = job.Client?.Name ?? "NA";

        // jobDTO.CategoryTitle = job.Category?.Title ?? "NA";

        var acceptedFreelancer = _unitOfWork.freelancerRepository.GetById(job.AcceptedFreelancerId ?? 0);

        //if (acceptedFreelancer is not null)
        //{
        //    jobDTO.AcceptedFreelancerId = acceptedFreelancer.Id;
        //    jobDTO.AcceptedFreelancerName = acceptedFreelancer.Name;
        //}

        jobDTO = _mapper.Map<Job, GetJobDTO>(job);

        return new GeneralResponse
        {
            IsSuccess = true,
            Data = jobDTO,
            Message = "Job is retrieved Successfully !"
        };
    }


    [HttpGet]
    public ActionResult<GeneralResponse> GetAll()
    {
        var jobs = _unitOfWork.jobRepository.FindAll(new string[] { "Category", "Client", "Skills" }).ToList();

        var jobDTOs = _mapper.Map<List<Job>, List<GetJobDTO>>(jobs);

        for (int i = 0; i < jobs.Count; i++)
        {
            //jobDTOs[i].ClientName = jobs[i].Client.Name;
            //jobDTOs[i].CategoryTitle = jobs[i].Category.Title;

            //var client =
            //    _unitOfWork.clientRepository.GetById(jobDTOs[i].ClientId);

            //var category =
            //    _unitOfWork.categoryRepository.GetById(jobDTOs[i].CategoryId);

            //var freelancer =
            //    _unitOfWork.freelancerRepository.GetById(jobDTOs[i].AcceptedFreelancerId);

            // it is calculated automatically in the prop from the count of proposal List 
            //jobDTOs[i].ProposalCount =
            //    _unitOfWork.proposalRepository.GetCount();

            var RateDTO = _mapper.Map<Rate, RateDTO>(jobs[i].Rate);

            foreach (var jobskill in jobs[i].Skills)
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
      (JobStatus? status, int? MinBudget, int? MaxBudget, int? ClientId, int? FreelancerId, bool? HasManyProposals, bool? IsNew, int page, int pageSize, PaginatedJobsRequestBodyDTO requestBody)
    {
        PaginationListDTO<GetJobDTO> paginatedJobs = new PaginationListDTO<GetJobDTO>();

        PaginationListDTO<Job> jobs = _unitOfWork.jobRepository
              .GetPaginatedJobs(status, MinBudget, MaxBudget, ClientId, FreelancerId, HasManyProposals, IsNew, page, pageSize, requestBody);

        paginatedJobs.Items = _mapper.Map<IEnumerable<Job>, IEnumerable<GetJobDTO>>(jobs.Items);
        paginatedJobs.TotalItems = jobs.TotalItems;
        paginatedJobs.CurrentPage = jobs.CurrentPage;
        paginatedJobs.TotalPages = jobs.TotalPages;

        if (paginatedJobs.Items is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Data = new PaginationListDTO<AddJobDTO>
                {
                    CurrentPage = paginatedJobs.CurrentPage,
                    TotalPages = paginatedJobs.TotalPages,
                    TotalItems = paginatedJobs.TotalItems,
                    Items = null
                },
                Status = 400,
                Message = "No Jobs Found with this filteration"
            };
        }

        List<GetJobDTO> jobsDTOs = (List<GetJobDTO>)paginatedJobs.Items;

        //foreach (var job in paginatedJobs.Items)
        //{
        //    var jobDTO = _mapper.Map<Job, AddJobDTO>(job);

        //    var client =
        //        _unitOfWork.clientRepository.GetById(jobDTO.ClientId);

        //    //jobDTO.ClientName = client?.Name ?? "NA";

        //    //var category =
        //    //    _unitOfWork.categoryRepository.GetById(jobDTO.CategoryId);
        //    //jobDTO.CategoryTitle = category?.Title ?? "NA";

        //    //var freelancer =
        //    //    _unitOfWork.freelancerRepository.GetById(jobDTO.AcceptedFreelancerId);
        //    //jobDTO.AcceptedFreelancerName = freelancer?.Name ?? "NA";

        //    //jobDTO.ProposalCount = _unitOfWork.proposalRepository.GetCount();

        //    jobDTOs.Add(jobDTO);
        //}

        return new GeneralResponse()
        {
            IsSuccess = true,
            Data = new PaginationListDTO<GetJobDTO>
            {
                CurrentPage = paginatedJobs.CurrentPage,
                TotalItems = paginatedJobs.TotalItems,
                TotalPages = paginatedJobs.TotalPages,
                Items = jobsDTOs
            },
            Status = 200,
        };
    }

    public async Task<ActionResult<GeneralResponse>> GetPaginatedJobsAsync
      (JobStatus? status, int? MinBudget, int? MaxBudget, int? ClientId, int? FreelancerId, bool? HasManyProposals, bool? IsNew, int page, int pageSize, PaginatedJobsRequestBodyDTO requestBody)
    {
        PaginationListDTO<GetJobDTO> paginatedJobs = new PaginationListDTO<GetJobDTO>();

        PaginationListDTO<Job> jobs = await _unitOfWork.jobRepository
              .GetPaginatedJobsAsync(status, MinBudget, MaxBudget, ClientId, FreelancerId, HasManyProposals, IsNew, page, pageSize, requestBody);

        paginatedJobs.Items = _mapper.Map<IEnumerable<Job>, IEnumerable<GetJobDTO>>(jobs.Items);
        paginatedJobs.TotalItems = jobs.TotalItems;
        paginatedJobs.CurrentPage = jobs.CurrentPage;
        paginatedJobs.TotalPages = jobs.TotalPages;

        //PaginatedListDTO<GetJobDTO> paginatedJobs = await _unitOfWork.jobRepository
        //      .GetPaginatedJobsAsync(status, MinBudget, MaxBudget, ClientId, FreelancerId, page, pageSize, requestBody)

        if (paginatedJobs.Items is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Data = new PaginationListDTO<AddJobDTO>()
                {
                    CurrentPage = paginatedJobs.CurrentPage,
                    TotalPages = paginatedJobs.TotalPages,
                    TotalItems = paginatedJobs.TotalItems,
                    Items = null
                },
                Status = 400,

                Message = "No Jobs Found with this filteration"
            };
        }

        var jobDTOs = new List<GetJobDTO>();

        //foreach (var job in paginatedJobs.Items)
        //{
        //    var jobDTO = _mapper.Map<Job, AddJobDTO>(job);

        //    //var client =
        //    //    _unitOfWork.clientRepository.GetById(jobDTO.ClientId);
        //    //jobDTO.ClientName = client?.Name ?? "NA";

        //    //var category =
        //    //    _unitOfWork.categoryRepository.GetById(jobDTO.CategoryId);
        //    //jobDTO.CategoryTitle = category?.Title ?? "NA";

        //    //var freelancer =
        //    //    _unitOfWork.freelancerRepository.GetById(jobDTO.AcceptedFreelancerId);
        //    //jobDTO.AcceptedFreelancerName = freelancer?.Name ?? "NA";

        //    // it is calculated automatically in the prop from the count of proposal List 
        //    //jobDTO.ProposalCount = _unitOfWork.proposalRepository.GetCount();

        //    jobDTOs.Add(jobDTO);
        //}

        return new GeneralResponse()
        {
            IsSuccess = true,
            Data = new PaginationListDTO<GetJobDTO>
            {
                CurrentPage = paginatedJobs.CurrentPage,
                TotalItems = paginatedJobs.TotalItems,
                TotalPages = paginatedJobs.TotalPages,
                Items = jobDTOs
            },
            Status = 200,
        };
    }


    [HttpGet("freelancer")]
    public ActionResult<GeneralResponse> GetByFreelancerId([FromQuery] int id)
    {
        List<Job> jobs;

        try
        {
            jobs = _unitOfWork.jobRepository.FindAll(new string[] { "Client", "Category", "Skills" }, j => j.AcceptedFreelancerId == id)
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


        var jobDTOs = _mapper.Map<List<Job>, List<GetJobDTO>>(jobs);
        for (int i = 0; i < jobs.Count; i++)
        {
            //jobDTOs[i].ClientName = jobs[i].Client.Name;
            //jobDTOs[i].CategoryTitle = jobs[i].Category.Title;
            var RateDTO = _mapper.Map<Rate, RateDTO>(jobs[i].Rate);
            jobDTOs[i].Rate = RateDTO;

            foreach (var jobSkill in jobs[i].Skills)
            {
                var skill = _unitOfWork.skillRepository.GetById(jobSkill.SkillId);
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


        var jobDTOs = _mapper.Map<List<Job>, List<GetJobDTO>>(jobs);


        for (int i = 0; i < jobs.Count; i++)
        {
            //jobDTOs[i].AcceptedFreelancerName = jobs[i]?.AcceptedFreelancer?.Name;
            //jobDTOs[i].AcceptedFreelancerId = jobs[i].?AcceptedFreelancer?.Id;
            //jobDTOs[i].CategoryTitle = jobs[i].?Category?.Title;

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
    public ActionResult<GeneralResponse> Add(AddJobDTO jobDTO)
    {
        var job = _mapper.Map<AddJobDTO, Job>(jobDTO);

        //foreach(SkillDTO skillDTO in jobDto.skillsDTO) 
        //{
        //    JobSkills skill = jobServiceSkills.Find(js => {js.SkillId == skillDTO.Id && js.JobId ==  });

        //    job.skills.Add(skill);
        //}

        try
        {
            //job.Skills = null;

            _unitOfWork.jobRepository.Add(job);
            _unitOfWork.Save();

            var JobSkills = new List<JobSkills>();

            foreach (int skillID in jobDTO.SkillsIds)
            {
                //var skill = new Skill()
                //{
                //    Title = skillDTO.Title,
                //    Description = skillDTO.Description,
                //};

                var skill = _unitOfWork.skillRepository.GetById(skillID);

                //_unitOfWork.Save();

                var jobSkill = new JobSkills
                {
                    SkillId = skill.Id,
                    JobId = job.Id
                };

                JobSkills.Add(jobSkill);
            }

            _unitOfWork.jobSkillsRepository.AddRange(JobSkills);
            _unitOfWork.jobSkillsRepository.Save();

            //foreach (GetProposalDTO getProposalDTO in jobDto.Proposals)
            //{
            //    Proposal proposal = mapper.Map<GetProposalDTO, Proposal>(getProposalDTO);

            //    job?.Proposals?.Add(proposal);

            //    //job.Proposals.Add(new JobSkills
            //    //{
            //    //    SkillId = skillDTO.Id,
            //    //    JobId = job.Id
            //    //});
            //}

            //Rate rate = mapper.Map<RateDTO , Rate>(jobDto.Rate);

            //job.Rate = rate;

            // job.skills = JobSkills;

            //_unitOfWork.jobRepository.Update(job);

            // _unitOfWork.Save();
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
            Message = "Job is added successfully",
            Status = 200
        };
    }


    [HttpPut]
    public ActionResult<GeneralResponse> Update(AddJobDTO jobDto)
    {
        var job = _unitOfWork.jobRepository.GetById(jobDto.Id);
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
        foreach (int skillID in jobDto.SkillsIds)
        {
            var skill = _unitOfWork.skillRepository.GetById(skillID);

            if (skill is not null)
            {
                skills.Add(new JobSkills
                {
                    SkillId = skill.Id,
                    JobId = jobDto.Id
                });
            }


        }
        //job.Skills = skills;


        try
        {
            _unitOfWork.jobSkillsRepository.AddRange(skills);

            // _unitOfWork.jobRepository.Update(job);
            //_unitOfWork.Save();

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

    [HttpGet("Search")]
    public async Task<ActionResult<GeneralResponse>> SearchByJobTitleAsync(string KeyWord)
    {
        IEnumerable<Job> jobs = new List<Job>();

        try
        {
            jobs = await _unitOfWork.
                jobRepository.FindAllAsync(criteria: j => j.Title.Contains(KeyWord), includes: new string[] { "Rate", "skills" });
        }
        catch (Exception ex)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Data = null,
                Message = "Error on searching for jobs match this key word"
            };
        }

        if (jobs.ToList() == null || jobs.ToList().Count == 0)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Data = null,
                Message = "No jobs match this search keyword"
            };
        }

        var getJobDTOs = _mapper.Map<IEnumerable<Job>, IEnumerable<GetJobDTO>>(jobs)
                                        .ToList();

        for (int i = 0; i < jobs.Count(); i++)
        {
            var rateId = jobs.ToList()[i].Rate?.Id;

            if (rateId != null)
            {
                var rate = await _unitOfWork.rateRepository.GetByIdAsync((int)rateId);

                if (rate is not null)
                {
                    var rateDTO = _mapper.Map<Rate, RateDTO>(rate);

                    getJobDTOs[i].Rate = rateDTO;
                }
            }


            var SkillDTOs = new List<SkillDTO>();

            foreach (var JobSkill in jobs.ToList()[i].Skills)
            {
                var skill = await
                    _unitOfWork.skillRepository.GetByIdAsync(JobSkill.SkillId);

                if (skill is not null)
                {
                    var skillDTO = _mapper.Map<Skill, SkillDTO>(skill);

                    SkillDTOs.Add(skillDTO);
                }
            }

            getJobDTOs[i].Skills = SkillDTOs;

        }
        return new GeneralResponse()
        {
            IsSuccess = true,
            Data = getJobDTOs,
            Message = "All Jobs match this key word retrieved Successfully !"
        };
    }
}
