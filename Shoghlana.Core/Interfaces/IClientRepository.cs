using Shoghlana.Core.Models;

namespace Shoghlana.Core.Interfaces;
public interface IClientRepository : IGenericRepository<Client>
{
    Client? GetClientWithJobs(int id);
}
