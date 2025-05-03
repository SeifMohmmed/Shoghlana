using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoghlana.API.Response;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;

namespace Shoghlana.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class JobController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public JobController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public ActionResult<GeneralResponse> GetAll()
    {
        var jobs=_unitOfWork.job.GetAll().ToList();

        return new GeneralResponse
        {
            IsSuccess = true,
            Data = jobs,
            Message = "All jobs retrieved successfully"
        };

    }
    [HttpGet("id")]
    public ActionResult<GeneralResponse> GetById(int id)
    {
        Job job = new Job();

        try
        {
            job=_unitOfWork.job.GetById(id);
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
            Data = job,
            Message = "Job is retrieved successfully"
        };
    }
}
