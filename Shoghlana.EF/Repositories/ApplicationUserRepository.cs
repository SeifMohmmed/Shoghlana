using Microsoft.AspNetCore.Identity;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;

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

    public async Task<IdentityResult> InsertAsync(ApplicationUser User, string Role, string Password = null)
    {
        IdentityResult result;

        if (Password == null)
        {
            result = await _userManager.CreateAsync(User);
        }

        else
        {
            result = await _userManager.CreateAsync(User, Password);
        }

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(User, Role);
        }

        return result;
    }

    //public async Task<IdentityResult> InsertWithPasswordAsync(ApplicationUser User, string Role, string Password) Add commentMore actions
    //{
    //    IdentityResult result = await userManager.CreateAsync(User, Password);
    //    await userManager.AddToRoleAsync(User, Role);
    //    return result;
    //}
}
