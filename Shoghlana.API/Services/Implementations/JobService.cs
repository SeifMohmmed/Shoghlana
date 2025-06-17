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

    public ActionResult<GeneralResponse> Get(int id)
    {
        var job =
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

        jobDTO = _mapper.Map<GetJobDTO>(job);

        if (job.Skills != null && job.Skills.Any())
        {
            var skillDTOs = new List<SkillDTO>();

            foreach (var jobskill in job.Skills)
            {
                var skill = _unitOfWork.skillRepository.GetById(jobskill.SkillId);

                if (skill is not null)
                {
                    var skillDTO = _mapper.Map<Skill, SkillDTO>(skill);

                    skillDTOs.Add(skillDTO);
                }
            }
            jobDTO.Skills = skillDTOs;
        }


        if (job.Proposals is not null)
        {
            var proposalDTOs = new List<GetProposalDTO>();

            foreach (var proposal in job.Proposals)
            {
                var proposalDTO = _mapper.Map<GetProposalDTO>(proposal);

                proposalDTOs.Add(proposalDTO);
            }

            jobDTO.Proposals = proposalDTOs;
        }

        jobDTO.ClientName = job.Client?.Name ?? "Anonymous user";


        var acceptedFreelancer = _unitOfWork.freelancerRepository.GetById(job.AcceptedFreelancerId ?? 0);

        if (acceptedFreelancer is not null)
        {
            jobDTO.AcceptedFreelancerId = acceptedFreelancer.Id;
            jobDTO.AcceptedFreelancerName = acceptedFreelancer.Name;
        }

        return new GeneralResponse
        {
            IsSuccess = true,
            Data = jobDTO,
            Message = "Job is retrieved Successfully !"
        };
    }


    public ActionResult<GeneralResponse> GetAll()
    {
        var jobs = _unitOfWork.jobRepository.FindAll(new string[] { "Category", "Client", "Skills" }).ToList();

        var jobDTOs = _mapper.Map<List<Job>, List<GetJobDTO>>(jobs);

        for (int i = 0; i < jobs.Count; i++)
        {
            var skillDTOs = new List<SkillDTO>();

            foreach (var jobskill in jobs[i].Skills)
            {
                var skill = _unitOfWork.skillRepository.GetById(jobskill.SkillId);

                jobDTOs[i].Skills.Add(new SkillDTO
                {
                    Title = skill.Title,
                    Id = skill.Id,
                });
            }
            jobDTOs[i].Skills = skillDTOs;
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
        var paginatedJobs = new PaginationListDTO<GetJobDTO>();

        requestBody.Includes = ["Proposals"];

        var jobs = _unitOfWork.jobRepository
              .GetPaginatedJobs(status, MinBudget, MaxBudget, ClientId, FreelancerId, HasManyProposals, IsNew, page, pageSize, requestBody);

        paginatedJobs.Items = _mapper.Map<IEnumerable<Job>, IEnumerable<GetJobDTO>>(jobs.Items);

        foreach (var jobDTO in paginatedJobs.Items)
        {
            if (jobDTO.ClientId > 0)
            {
                jobDTO.ClientName = _unitOfWork.clientRepository.Find(criteria: c => c.Id == jobDTO.ClientId)?.Name ?? "NA";
            }


            if (jobDTO.AcceptedFreelancerId > 0)
            {
                jobDTO.AcceptedFreelancerName = _unitOfWork.freelancerRepository.Find(criteria: f => f.Id == jobDTO.AcceptedFreelancerId)?.Name ?? "NA";
            }

            jobDTO.ProposalsCount = jobDTO?.Proposals?.Count ?? 0;

            // then I don't need the proposal list any more to make the payload lighterAdd commentMore actions
            jobDTO.Proposals = null;

        }

        paginatedJobs.TotalItems = jobs.TotalItems;
        paginatedJobs.CurrentPage = jobs.CurrentPage;
        paginatedJobs.TotalPages = jobs.TotalPages;

        if (jobs.Items is null)
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

        return new GeneralResponse()
        {
            IsSuccess = true,
            Data = new PaginationListDTO<GetJobDTO>
            {
                CurrentPage = paginatedJobs.CurrentPage,
                TotalItems = paginatedJobs.TotalItems,
                TotalPages = paginatedJobs.TotalPages,
                Items = paginatedJobs.Items
            },
            Status = 200,
        };
    }

    public async Task<ActionResult<GeneralResponse>> GetPaginatedJobsAsync
      (JobStatus? status, int? MinBudget, int? MaxBudget, int? ClientId, int? FreelancerId, bool? HasManyProposals, bool? IsNew, int page, int pageSize, PaginatedJobsRequestBodyDTO requestBody)
    {
        var paginatedJobs = new PaginationListDTO<GetJobDTO>();

        var jobs = await _unitOfWork.jobRepository
              .GetPaginatedJobsAsync(status, MinBudget, MaxBudget, ClientId, FreelancerId, HasManyProposals, IsNew, page, pageSize, requestBody);

        paginatedJobs.Items = _mapper.Map<IEnumerable<Job>, IEnumerable<GetJobDTO>>(jobs.Items);
        paginatedJobs.TotalItems = jobs.TotalItems;
        paginatedJobs.CurrentPage = jobs.CurrentPage;
        paginatedJobs.TotalPages = jobs.TotalPages;

        //PaginatedListDTO<GetJobDTO> paginatedJobs = await _unitOfWork.jobRepository
        //      .GetPaginatedJobsAsync(status, MinBudget, MaxBudget, ClientId, FreelancerId, page, pageSize, requestBody)

        if (jobs.Items is null)
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

        return new GeneralResponse()
        {
            IsSuccess = true,
            Data = new PaginationListDTO<GetJobDTO>
            {
                CurrentPage = paginatedJobs.CurrentPage,
                TotalItems = paginatedJobs.TotalItems,
                TotalPages = paginatedJobs.TotalPages,
                Items = paginatedJobs.Items
            },
            Status = 200,
        };
    }


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

        if (jobs != null && jobs.Any())
        {
            var jobDTOs = _mapper.Map<List<Job>, List<GetJobDTO>>(jobs);

            for (int i = 0; i < jobs.Count; i++)
            {
                if (jobs[i].Rate is not null)
                {
                    var RateDTO = _mapper.Map<Rate, RateDTO>(jobs[i].Rate);
                    jobDTOs[i].Rate = RateDTO;
                }

                if (jobs[i].Skills is not null && jobs[i].Skills.Any())
                {
                    var SkillDTOs = new List<SkillDTO>();

                    foreach (var jobSkill in jobs[i].Skills)
                    {
                        var skill = _unitOfWork.skillRepository.GetById(jobSkill.SkillId);

                        SkillDTOs.Add(new SkillDTO
                        {
                            Title = skill.Title,
                            Id = skill.Id
                        });
                    }
                    jobDTOs[i].Skills = SkillDTOs;
                }
            }

            return new GeneralResponse
            {
                IsSuccess = true,
                Data = jobDTOs,
                Message = "All jobs for this freelancer retrieved successfully"
            };
        }

        return new GeneralResponse()
        {
            IsSuccess = false,
            Data = null,
            Message = "No jobs found for this freelancer"
        };
    }

    public ActionResult<GeneralResponse> GetJobsByCategoryId(int id)
    {
        var Jobs = _unitOfWork.jobRepository.GetByCategoryId(id);

        if (Jobs != null && Jobs.Any())
        {
            var JobDTOs = _mapper.Map<List<GetJobDTO>>(Jobs);

            foreach (GetJobDTO job in JobDTOs)
            {
                Client? client = _unitOfWork.clientRepository.GetById(job.ClientId);

                if (client != null)
                {
                    job.ClientName = client.Name;
                }

                if (job.AcceptedFreelancerId != null)
                {
                    Freelancer? freelancer = _unitOfWork.freelancerRepository
                                             .GetById((int)job.AcceptedFreelancerId);

                    job.AcceptedFreelancerName = freelancer?.Name;
                }

                Category? category = _unitOfWork.categoryRepository.GetById(job.CategoryId);
                job.CategoryTitle = category?.Title;
            }

            return new GeneralResponse()
            {
                IsSuccess = true,
                Status = 200,
                Data = JobDTOs
            };
        }

        return new GeneralResponse()
        {
            IsSuccess = false,
            Status = 400,
            Message = "No jobs Found for this category !"
        };
    }


    public ActionResult<GeneralResponse> GetJobsByCategoryIds([FromQuery] List<int> ids)
    {
        var jobs = new List<GetJobDTO>();

        foreach (int id in ids)
        {
            var tempJobs = new List<Job>();

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
            if (tempJobs != null && tempJobs.Any())
            {
                var TempGetJobDtos = _mapper.Map<List<Job>, List<GetJobDTO>>(tempJobs);
                jobs.AddRange(TempGetJobDtos);
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


    public ActionResult<GeneralResponse> GetByClientId([FromRoute] int id)
    {
        List<Job> jobs;

        try
        {
            jobs = _unitOfWork.jobRepository.FindAll(new string[] { "AcceptedFreelancer", "Category", "Skills" }, j => j.ClientId == id)
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

        if (jobs.Any())
        {
            var jobDTOs = _mapper.Map<List<Job>, List<GetJobDTO>>(jobs);

            for (int i = 0; i < jobs.Count; i++)
            {
                jobDTOs[i].AcceptedFreelancerName = jobs[i]?.AcceptedFreelancer?.Name;
                jobDTOs[i].AcceptedFreelancerId = jobs[i]?.AcceptedFreelancer?.Id;

                jobDTOs[i].CategoryTitle = jobs[i]?.Category?.Title;

                var SkillDTOs = new List<SkillDTO>();

                foreach (JobSkills jobSkill in jobs[i].Skills)
                {
                    var skill = _unitOfWork.skillRepository.GetById(jobSkill.SkillId);

                    jobDTOs[i].Skills.Add(new SkillDTO
                    {
                        Title = skill.Title,
                        Id = skill.Id,
                    });
                }
                jobDTOs[i].Skills = SkillDTOs;
            }
            return new GeneralResponse
            {
                IsSuccess = true,
                Data = jobDTOs,
                Message = "All jobs for this client retrieved successfully"
            };
        }

        return new GeneralResponse()
        {
            IsSuccess = false,
            Data = null,
            Message = "No jobs found for this client"
        };
    }

    public ActionResult<GeneralResponse> Add(AddJobDTO jobDTO)
    {
        var job = _mapper.Map<AddJobDTO, Job>(jobDTO);

        try
        {
            _unitOfWork.jobRepository.Add(job);
            _unitOfWork.Save();

            var JobSkills = new List<JobSkills>();

            if (jobDTO.SkillsIds is not null && jobDTO.SkillsIds.Count > 0)
            {
                foreach (int skillID in jobDTO.SkillsIds)
                {

                    var skill = _unitOfWork.skillRepository.GetById(skillID);

                    if (skill is not null)
                    {
                        var jobSkill = new JobSkills
                        {
                            SkillId = skill.Id,
                            JobId = job.Id
                        };

                        JobSkills.Add(jobSkill);
                    }
                }


                if (JobSkills.Any())
                {
                    _unitOfWork.jobSkillsRepository.AddRange(JobSkills);
                    _unitOfWork.jobSkillsRepository.Save();
                }

            }
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

    public ActionResult<GeneralResponse> Update(AddJobDTO jobDto)
    {
        var job = _unitOfWork.jobRepository.GetById((int)jobDto.Id);

        job.Id = (int)jobDto.Id;
        job.Description = jobDto.Description;
        job.Title = jobDto.Title;
        job.MaxBudget = jobDto.MaxBudget;
        job.MinBudget = jobDto.MinBudget;
        job.ExperienceLevel = jobDto.ExperienceLevel;
        job.CategoryId = jobDto.CategoryId;
        job.PostTime = (DateTime)jobDto.PostTime;
        job.Status = (JobStatus)jobDto.Status;
        job.DurationInDays = jobDto.DurationInDays;
        job.ExperienceLevel = jobDto.ExperienceLevel;
        //job.ClientId = jobDto.ClientId;

        var jobSkills = _unitOfWork.jobSkillsRepository
                               .FindAll(criteria: js => js.JobId == jobDto.Id)
                               .ToList();

        _unitOfWork.jobSkillsRepository.DeleteRange(jobSkills);

        var skills = new List<JobSkills>();

        foreach (int skillID in jobDto.SkillsIds)
        {
            var skill = _unitOfWork.skillRepository.GetById(skillID);

            if (skill is not null)
            {
                skills.Add(new JobSkills
                {
                    SkillId = skill.Id,
                    JobId = (int)jobDto.Id
                });
            }


        }
        if (skills is not null)
        {

            try
            {
                _unitOfWork.jobSkillsRepository.AddRange(skills);

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

        _unitOfWork.Save();
        return new GeneralResponse
        {
            IsSuccess = true,
            Data = jobDto,
            Message = "Job updated successfully"
        };
    }


    public ActionResult<GeneralResponse> Delete(int id)
    {
        var job = _unitOfWork.jobRepository.GetById(id);

        if (job is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Data = id,
                Message = $"No Job found with this ID : {id}",
                Status = 404,
            };
        }

        var jobSkills = _unitOfWork.jobSkillsRepository
                                   .FindAll(criteria: js => js.JobId == id)
                                   .ToList();

        if (jobSkills.Count > 0)
        {
            _unitOfWork.jobSkillsRepository.DeleteRange(jobSkills);
        }


        var proposals = _unitOfWork.proposalRepository
                                  .FindAll(criteria: p => p.JobId == id)
                                  .ToList();

        if (proposals.Count > 0)
        {
            foreach (Proposal proposal in proposals)
            {
                var images = _unitOfWork.proposalImageRepository
                                          .FindAll(criteria: pi => pi.ProposalId == proposal.Id)
                                          .ToList();

                _unitOfWork.proposalImageRepository.DeleteRange(images);
            }

            _unitOfWork.proposalRepository.DeleteRange(proposals);
        }


        var rate = _unitOfWork.rateRepository.Find(criteria: r => r.JobId == id);

        if (rate != null)
        {
            _unitOfWork.rateRepository.Delete(rate);
        }

        try
        {
            _unitOfWork.jobRepository.Delete(job);
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

        return new GeneralResponse
        {
            IsSuccess = true,
            Data = null,
            Message = "Job was deleted successfully"
        };
    }

    public async Task<ActionResult<GeneralResponse>> SearchByJobTitleAsync(string KeyWord)
    {
        IList<Job> Jobs = (IList<Job>)await _unitOfWork.jobRepository
                  .FindAllAsync(criteria: j => j.Title.Contains(KeyWord), includes: new string[] { "Rate", "Skills" });


        if (Jobs.Count() == 0)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Data = null,
                Message = "No jobs match this search key word"
            };
        }


        var GetJobDtos = _mapper.Map<IEnumerable<Job>, IEnumerable<GetJobDTO>>(Jobs)
                                          .ToList();

        for (int i = 0; i < Jobs.Count(); i++)
        {
            int? RateId = Jobs[i].Rate?.Id;

            if (RateId is not null)
            {
                var rate = await _unitOfWork.rateRepository.GetByIdAsync((int)RateId);
                if (rate is not null)
                {
                    var RateDto = _mapper.Map<Rate, RateDTO>(rate);
                    GetJobDtos[i].Rate = RateDto;
                }
            }


            var SkillDTOs = new List<SkillDTO>();

            foreach (var JobSkill in Jobs[i].Skills)
            {
                var skill = await _unitOfWork.skillRepository.GetByIdAsync(JobSkill.SkillId);

                if (skill is not null)
                {
                    var SkillDto = _mapper.Map<Skill, SkillDTO>(skill);
                    SkillDTOs.Add(SkillDto);
                }
            }

            GetJobDtos[i].Skills = SkillDTOs;
        }

        return new GeneralResponse()
        {
            IsSuccess = true,
            Data = GetJobDtos,
            Message = "All jobs match this key word retrieved successfully"
        };
    }
}

