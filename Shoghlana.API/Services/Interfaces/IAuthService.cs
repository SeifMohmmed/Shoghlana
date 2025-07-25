using Shoghlana.API.Response;
using Shoghlana.Application.DTOs;
using Shoghlana.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace Shoghlana.API.Services.Interfaces;
public interface IAuthService
{
    Task<AuthModel> RegisterAsync(RegisterModel model);

    Task<AuthModel> GetTokenAsync(TokenRequestModel model);

    Task<string> AddRoleAsync(AddRoleModel model);

    Task<AuthModel> RefreshTokenAsync(string token);

    Task<bool> RevokeTokenAsync(string token);

    Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user);

    //Reset Password 
    Task<AuthModel> ForgotPasswordAsync(string email);

    Task<AuthModel> ResetPasswordAsync(ResetPasswordRequest request);


    //Google Authentication
    Task<ApplicationUser> GetByIdAsync(string id);

    Task<ApplicationUser> GetByEmailAsync(string email);

    Task<GeneralResponse> GoogleAuthenticationAsync(GoogleSignupDTO googleSignupDto);

    Task<GeneralResponse> IsGmailTokenValidAsync(string GmailToken);


}
