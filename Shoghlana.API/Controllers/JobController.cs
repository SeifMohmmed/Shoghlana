using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoghlana.Core.DTOs;
using Shoghlana.API.Response;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;
using Shoghlana.EF.Repositories;
using Shoghlana.API.Services.Interfaces;

namespace Shoghlana.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class JobController : ControllerBase
{
    private readonly IJobService _jobService;

    public JobController(IJobService jobService)
    {
       _jobService = jobService;
    }


    [HttpGet]
    public ActionResult<GeneralResponse> GetAll()
    {
        return _jobService.GetAll();
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
    public ActionResult<GeneralResponse> Add(JobDTO jobDTO)
    {
        return _jobService.Add(jobDTO);
    }


    [HttpPut]
    public ActionResult<GeneralResponse> Update(JobDTO jobDto)
    {
        return _jobService.Update(jobDto);
    }


    [HttpDelete("{id:int}")]
    public ActionResult<GeneralResponse> Delete(int id)
    {
        return _jobService.Delete(id);
    }
}
