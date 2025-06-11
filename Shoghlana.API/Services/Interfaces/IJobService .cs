using Microsoft.AspNetCore.Mvc;
using Shoghlana.API.Response;
using Shoghlana.API.Services.Implementations;
using Shoghlana.Core.DTOs;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;
using Shoghlana.EF.Repositories;

namespace Shoghlana.API.Services.Interfaces;

public interface IJobService : IGenericService<Job>
{
    public ActionResult<GeneralResponse> GetAll();

    public ActionResult<GeneralResponse> GetPaginatedJobs(
        int MinBudget, int MaxBudget, int CategoryId, int ClientId, int FreelancerId,
        int page, int pageSize, string[] includes = null);

    public Task<ActionResult<GeneralResponse>> GetPaginatedJobsAsync(
        int MinBudget, int MaxBudget, int CategoryId, int ClientId, int FreelancerId,
        int page, int pageSize, string[] includes = null);

    public ActionResult<GeneralResponse> Get(int id);

    public ActionResult<GeneralResponse> GetByFreelancerId([FromQuery] int id);

    public ActionResult<GeneralResponse> GetJobsByCategoryId(int id);

    public ActionResult<GeneralResponse> GetJobsByCategoryIds([FromQuery] List<int> ids);

    public ActionResult<GeneralResponse> GetByClientId([FromQuery] int id);

    public ActionResult<GeneralResponse> Add(JobDTO jobDTO);

    public ActionResult<GeneralResponse> Update(JobDTO jobDTO);

    public ActionResult<GeneralResponse> Delete(int id);
}
