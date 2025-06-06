using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;
using Shoghlana.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.SignalR;
using Shoghlana.EF.Hubs;
using Shoghlana.Core.DTOs;

namespace Shoghlana.EF.Repositories;
public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JWT _jwt;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IHubContext<NotificationHub> _hubContext;

    public AuthService(UserManager<ApplicationUser> userManager, IOptions<JWT> jwt, RoleManager<IdentityRole> roleManager
        , IUnitOfWork unitOfWork, IHubContext<NotificationHub> hubContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _hubContext = hubContext;
        _jwt = jwt.Value;
    }
    public async Task<AuthModel> RegisterAsync(RegisterModel model)
    {
        if (await _userManager.FindByEmailAsync(model.Email) is not null)
        {
            return new AuthModel { Message = "Email is already registered!" };
        }
        if (await _userManager.FindByNameAsync(model.Username) is not null)
        {
            return new AuthModel { Message = "Username is already registered!" };
        }

        var user = new ApplicationUser
        {
            UserName = model.Username,
            Email = model.Email,
        };

        var result =
            await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return new AuthModel { Message = errors };
        }

        // Send a welcome notification to the user
        await SendWelcomeNotificationAsync(user);

        var jwtSecurityToken = await CreateJwtToken(user);

        // Determine the user's roles
        var roles = await _userManager.GetRolesAsync(user);

        return new AuthModel
        {
            Email = user.Email,
            ExpiresOn = jwtSecurityToken.ValidTo,
            IsAuthenticated = true,
            Roles = roles.ToList(),
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Username = user.UserName,
        };
    }

    private async Task SendWelcomeNotificationAsync(ApplicationUser user)
    {
        var notification = new NotificationDTO
        {
            Title = "Welcome to Shoglana!",
            Description = $"Welcome, {user.UserName}! Thank you for joining us.",
            SentTime = DateTime.Now,
            // You can include the user's image in the notification if available

        };

        await _hubContext.Clients.User(user.Id).SendAsync("ReceiveNotification", notification);
    }

    public async Task<string> AddRoleAsync(AddRoleModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);

        if (user is null || await _roleManager.RoleExistsAsync(model.Role))
        {
            return "Invalid User ID or Role";
        }

        if (await _userManager.IsInRoleAsync(user, model.Role))
        {
            return "User Already Assigned To This Role";
        }

        var result = await _userManager.AddToRoleAsync(user, model.Role);

        return result.Succeeded ? string.Empty : "Something Went Wrong!";
    }

    public async Task<AuthModel> GetTokenAsync(TokenRequestModel model)
    {
        var authModel = new AuthModel();

        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user is null ||
            !await _userManager.CheckPasswordAsync(user, model.Password))
        {
            authModel.Message = "Email Or Password is Incorrect!";

            return authModel;
        }
        var jwtSecurityToken = await CreateJwtToken(user);

        var roleList = await _userManager.GetRolesAsync(user);

        authModel.IsAuthenticated = true;
        authModel.Token =
            new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        authModel.Email = user.Email;
        authModel.Username = user.UserName;
        authModel.ExpiresOn = jwtSecurityToken.ValidTo;
        authModel.Roles = roleList.ToList();

        return authModel;
    }




    public async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);
        var rolesClaims = new List<Claim>();

        foreach (var role in roles)
            rolesClaims.Add(new Claim("roles", role));

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email,user.Email),
            new Claim("uid",user.Id)
        }
        .Union(userClaims)
        .Union(rolesClaims);

        var symmetricSecurityKey = new
            SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));

        var signingCredentials = new
            SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.Now.AddDays(_jwt.DurationInDays),
            signingCredentials: signingCredentials
            );
        return jwtSecurityToken;
    }
}
