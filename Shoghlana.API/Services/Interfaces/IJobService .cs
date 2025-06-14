using Microsoft.AspNetCore.Mvc;
using Shoghlana.API.Response;
using Shoghlana.API.Services.Implementations;
using Shoghlana.Core.DTOs;
using Shoghlana.Core.Enums;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;
using Shoghlana.EF.Repositories;

namespace Shoghlana.API.Services.Interfaces;

public interface IJobService : IGenericService<Job>
{
    public ActionResult<GeneralResponse> GetAll();

    public ActionResult<GeneralResponse> GetPaginatedJobs
      (JobStatus? status, int? MinBudget, int? MaxBudget, int? ClientId, int? FreelancerId, bool? HasManyProposals, bool? IsNew,
        int page, int pageSize, PaginatedJobsRequestBodyDTO requestBody);


    public Task<ActionResult<GeneralResponse>> GetPaginatedJobsAsync
      (JobStatus? status, int? MinBudget, int? MaxBudget, int? ClientId, int? FreelancerId, bool? HasManyProposals, bool? IsNew, int page, int pageSize, PaginatedJobsRequestBodyDTO requestBody);


    public ActionResult<GeneralResponse> Get(int id);

    public ActionResult<GeneralResponse> GetByFreelancerId([FromQuery] int id);

    public ActionResult<GeneralResponse> GetJobsByCategoryId(int id);

    public ActionResult<GeneralResponse> GetJobsByCategoryIds([FromQuery] List<int> ids);

    public ActionResult<GeneralResponse> GetByClientId([FromQuery] int id);

    public ActionResult<GeneralResponse> Add(AddJobDTO jobDTO);

    public ActionResult<GeneralResponse> Update(AddJobDTO jobDTO);

    public ActionResult<GeneralResponse> Delete(int id);

    public Task<ActionResult<GeneralResponse>> SearchByJobTitleAsync(string KeyWord);


}
