using Microsoft.EntityFrameworkCore;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;

namespace Shoghlana.EF.Repositories;
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
