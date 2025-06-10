using Microsoft.AspNetCore.Identity;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.EF.Repositories;
public class ApplicationUserRepository : IApplicationUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ApplicationUserRepository(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApplicationUser> GetByEmailAsync(string email)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(email);

        return user;
    }

    public async Task<ApplicationUser> GetByIdAsync(string id)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(id);

        return user;
    }

    public async Task<IdentityResult> InsertAsync(ApplicationUser User)
    {
        IdentityResult result = await _userManager.CreateAsync(User);

        return result;
    }
}
