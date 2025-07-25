using Microsoft.EntityFrameworkCore;
using Shoghlana.Domain.Entities;
using Shoghlana.Domain.Repositories;
using Shoghlana.Infrastructure.Persistence;

namespace Shoghlana.Infrastructure.Repositories;
public class ClientRepository : GenericRepository<Client>, IClientRepository
{
    private readonly ApplicationDbContext _context;
    public ClientRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public Client? GetClientWithJobs(int id)
    {
        return _context.Clients.Include(c => c.Jobs)
            .FirstOrDefault(c => c.Id == id);
    }
}
