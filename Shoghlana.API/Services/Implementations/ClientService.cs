using Shoghlana.API.Services.Interfaces;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;

namespace Shoghlana.API.Services.Implementations;

public class ClientService : GenericService<Client>,IClientService
{
    public ClientService(IUnitOfWork unitOfWork,IGenericRepository<Client> repository)
        : base(unitOfWork, repository)
    {

    }

}
