using Microsoft.AspNetCore.Identity;
using Shoghlana.Core.Models;

namespace Shoghlana.Core.Interfaces;
public interface IApplicationUserRepository
{
    Task<ApplicationUser> GetByIdAsync(string id);

    Task<ApplicationUser> GetByEmailAsync(string email);

    Task<IdentityResult> InsertAsync(ApplicationUser User, string Role, string Password = null);

}
