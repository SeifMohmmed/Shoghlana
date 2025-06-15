using Shoghlana.Core.DTOs;
using Shoghlana.Core.Enums;
using Shoghlana.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.Core.Interfaces;
public interface IJobRepository : IGenericRepository<Job>
{
    public PaginationListDTO<Job> GetPaginatedJobs
      (JobStatus? status, int? MinBudget, int? MaxBudget, int? ClientId, int? FreelancerId, bool? HasManyProposals, bool? IsNew, int page, int pageSize, PaginatedJobsRequestBodyDTO requestBody);

    public Task<PaginationListDTO<Job>> GetPaginatedJobsAsync
      (JobStatus? status, int? MinBudget, int? MaxBudget, int? ClientId, int? FreelancerId, bool? HasManyProposals, bool? IsNew, int page, int pageSize, PaginatedJobsRequestBodyDTO requestBody);

    public List<Job> GetByCategoryId(int id);

}
