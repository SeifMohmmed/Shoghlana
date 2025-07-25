using Shoghlana.Domain.DTOs;
using Shoghlana.Domain.Entities;
using Shoghlana.Domain.Enums;

namespace Shoghlana.Domain.Repositories;
public interface IJobRepository : IGenericRepository<Job>
{
    public PaginationListDTO<Job> GetPaginatedJobs
      (JobStatus? status, int? MinBudget, int? MaxBudget, int? ClientId, int? FreelancerId, bool? HasManyProposals, bool? IsNew, int page, int pageSize, PaginatedJobsRequestBodyDTO requestBody);

    public Task<PaginationListDTO<Job>> GetPaginatedJobsAsync
      (JobStatus? status, int? MinBudget, int? MaxBudget, int? ClientId, int? FreelancerId, bool? HasManyProposals, bool? IsNew, int page, int pageSize, PaginatedJobsRequestBodyDTO requestBody);

    public List<Job> GetByCategoryId(int id);

}
