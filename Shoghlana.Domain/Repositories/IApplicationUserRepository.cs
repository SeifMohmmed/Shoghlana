using Microsoft.AspNetCore.Identity;
using Shoghlana.Domain.Entities;

namespace Shoghlana.Domain.Repositories;
public interface IApplicationUserRepository
{
    Task<ApplicationUser> GetByIdAsync(string id);

    Task<ApplicationUser> GetByEmailAsync(string email);

    Task<IdentityResult> InsertAsync(ApplicationUser User, string Role, string Password = null);

}
