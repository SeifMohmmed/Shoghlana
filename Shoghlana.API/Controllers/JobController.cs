using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoghlana.Core.DTOs;
using Shoghlana.API.Response;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;

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

            foreach (Skill skill in jobs[i].Skills)
            {
                jobDTOs[i].SkillsDic.Add(skill.Id, skill.Title);
            }
        }

        return new GeneralResponse
        {
            IsSuccess = true,
            Data = jobDTOs,
            Message = "All jobs retrieved successfully"
        };

    }
    [HttpGet("id")]
    public ActionResult<GeneralResponse> GetById(int id)
    {
        var job = new Job();

        var jobDTOs = new JobDTO();

        try
        {
            job = _unitOfWork.job.Find(new string[] { "Skills", "Category", "Proposals", "Client" },j=>j.Id==id);

            jobDTOs = _mapper.Map<Job, JobDTO>(job);
            jobDTOs.ClientName = job.Client.Name;
            jobDTOs.CategoryTitle = job.Category.Title;

            foreach (var proposal in job.Proposals)
            {
                var freelancer = _unitOfWork.freelancer.GetById(proposal.FreelancerId);
                jobDTOs.FreelancerDic.Add(freelancer.Id, freelancer.Name);
                jobDTOs.ProposalDic.Add(proposal.Id, proposal.Description);
            }
            foreach (var skill in job.Skills)
            {
                jobDTOs.SkillsDic.Add(skill.Id, skill.Title);
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
    [HttpPost]
    public ActionResult<GeneralResponse> Add(JobDTO jobDTO)
    {
        var job = _mapper.Map<JobDTO, Job>(jobDTO);
        return new GeneralResponse()
        {
            IsSuccess = true,
            Data = jobDTO,
            Message = "Job is added successfully"
        };
    }
}
