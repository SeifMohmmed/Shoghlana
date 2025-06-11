using Shoghlana.Core.DTOs;
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
               (int MinBudget, int MaxBudget, int CategoryId, int ClientId, int FreelancerId,
                int page, int pageSize, string[] includes = null);

    public Task<PaginationListDTO<Job>> GetPaginatedJobsAsync
               (int MinBudget, int MaxBudget, int CategoryId, int ClientId, int FreelancerId,
                int page, int pageSize, string[] includes = null);


}
