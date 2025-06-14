using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoghlana.API.Response;
using Shoghlana.API.Services.Implementations;
using Shoghlana.API.Services.Interfaces;
using Shoghlana.Core.DTOs;
using Shoghlana.Core.Enums;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;
using Shoghlana.EF.Repositories;

namespace Shoghlana.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[EnableCors("AllowAngular")]// Apply the Angular-specific CORS policy to this controller
public class JobController : ControllerBase
{
    private readonly IJobService _jobService;

    private const int defaultPageNumber = 1;

    private const int defaultPageSize = 5;

    public JobController(IJobService jobService)
    {
        _jobService = jobService;
    }


    [HttpGet]
    public ActionResult<GeneralResponse> GetAll()
    {
        return _jobService.GetAll();
    }

    [HttpGet("pagination")]
    public ActionResult<GeneralResponse> GetPaginatedJobs
    (int? MinBudget, int? MaxBudget, int? ClientId, int? FreelancerId, bool? HasManyProposals, bool? IsNew, int page = defaultPageNumber, int pageSize = defaultPageSize, JobStatus? status = JobStatus.All, PaginatedJobsRequestBodyDTO requestBody = null)
    {
        return _jobService
         .GetPaginatedJobs(status, MinBudget, MaxBudget, ClientId, FreelancerId, HasManyProposals, IsNew, page, pageSize, requestBody);
    }

    [HttpGet("paginationAsync")]
    public async Task<ActionResult<GeneralResponse>> GetPaginatedJobsAsync
      (JobStatus? status, int? MinBudget, int? MaxBudget, int? ClientId, int? FreelancerId, bool? HasManyProposals, bool? IsNew, int page, int pageSize, PaginatedJobsRequestBodyDTO requestBody)
    {
        return await _jobService.GetPaginatedJobsAsync(status, MinBudget, MaxBudget, ClientId, FreelancerId, HasManyProposals, IsNew, page, pageSize, requestBody);
    }


    [HttpGet("{id:int}")]
    public ActionResult<GeneralResponse> GetById(int id)
    {
        return _jobService.Get(id);
    }


    [HttpGet("freelancer")]
    public ActionResult<GeneralResponse> GetByFreelancerId([FromQuery] int id)
    {
        return _jobService.GetByFreelancerId(id);
    }


    [HttpGet("category/{id:int}")]
    public ActionResult<GeneralResponse> GetJobByCategoryId(int id)
    {
        return _jobService.GetJobsByCategoryId(id);
    }


    [HttpGet("categories")]
    public ActionResult<GeneralResponse> GetJobsByCategoryIds([FromQuery] List<int> ids)
    {
        return _jobService.GetJobsByCategoryIds(ids);
    }


    [HttpGet("client")]
    public ActionResult<GeneralResponse> GetByClientId([FromQuery] int id)
    {
        return _jobService.GetByClientId(id);
    }

    [HttpPost]
    public ActionResult<GeneralResponse> Add(AddJobDTO jobDTO)
    {
        return _jobService.Add(jobDTO);
    }


    [HttpPut]
    public ActionResult<GeneralResponse> Update(AddJobDTO jobDto)
    {
        return _jobService.Update(jobDto);
    }


    [HttpDelete("{id:int}")]
    public ActionResult<GeneralResponse> Delete(int id)
    {
        return _jobService.Delete(id);
    }

    [HttpGet("Search")]
    public async Task<ActionResult<GeneralResponse>> SearchByJobTitleAsync(string KeyWord)
    {
        if (KeyWord == null || KeyWord == "")
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Data = null,
                Message = "No keyword to use in search"
            };
        }

        return await _jobService.SearchByJobTitleAsync(KeyWord);
    }
}
