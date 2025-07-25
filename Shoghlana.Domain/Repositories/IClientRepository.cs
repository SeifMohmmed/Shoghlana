using Shoghlana.Domain.Entities;

namespace Shoghlana.Domain.Repositories;
public interface IClientRepository : IGenericRepository<Client>
{
    Client? GetClientWithJobs(int id);
}
