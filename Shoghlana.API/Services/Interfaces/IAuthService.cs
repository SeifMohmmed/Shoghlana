using Shoghlana.API.Response;
using Shoghlana.Core.DTOs;
using Shoghlana.Core.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.API.Services.Interfaces;
public interface IAuthService
{
    Task<AuthModel> RegisterAsync(RegisterModel model);

    Task<AuthModel> GetTokenAsync(TokenRequestModel model);

    Task<string> AddRoleAsync(AddRoleModel model);

    Task<AuthModel> RefreshTokenAsync(string token);

    Task<bool> RevokeTokenAsync(string token);

    Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user);


    //Google Authentication
    Task<ApplicationUser> GetByIdAsync(string id);

    Task<ApplicationUser> GetByEmailAsync(string email);

    Task<GeneralResponse> GoogleAuthenticationAsync(GoogleSignupDTO googleSignupDto);

    Task<GeneralResponse> IsGmailTokenValidAsync(string GmailToken);


}
