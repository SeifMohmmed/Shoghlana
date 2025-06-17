﻿using Google.Apis.Auth;
using Google.Apis.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shoghlana.API.Response;
using Shoghlana.API.Services.Interfaces;
using Shoghlana.Core.DTOs;
using Shoghlana.Core.Enums;
using Shoghlana.Core.Helpers;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;
using Shoghlana.EF.Configurations;
using Shoghlana.EF.Hubs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Google.Apis.Auth.GoogleJsonWebSignature;
using IClientService = Shoghlana.API.Services.Interfaces.IClientService;

namespace Shoghlana.API.Services.Implementations;
public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JWT _jwt;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly GoogleAuthConfig _googleAuthConfig;
    private readonly IFreelancerService _freelancerService;
    private readonly IClientService _clientService;

    public AuthService
        (UserManager<ApplicationUser> userManager, IOptions<JWT> jwt,
        RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork, IHubContext<NotificationHub> hubContext,
        IFreelancerService freelancerService, IOptions<GoogleAuthConfig> googleAuthConfig, IClientService clientService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _unitOfWork = unitOfWork;
        _hubContext = hubContext;
        _freelancerService = freelancerService;
        _clientService = clientService;
        _googleAuthConfig = googleAuthConfig.Value;
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

        var user = new ApplicationUser();

        if (model.Role == (int)UserRole.Freelancer)
        {
            var freelancer = new Freelancer()
            {
                Name = model.Username
                // convert img from string to bytes and save it in freelancer
            };

            _freelancerService.Add(freelancer);   // add + save inside the same method

            user.UserName = model.Username;

            user.Email = model.Email;

            user.FreelancerId = freelancer.Id;
            //PhoneNumber = model.PhoneNumber,

            //NormalizedEmail = model.Email ,

            //PasswordHash = model.Password,

            // TODO the mail and password ? 


            var result =
                await _unitOfWork.ApplicationUserRepository.InsertAsync(user, "Freelancer", model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new AuthModel { Message = errors };
            }
        }

        else if (model.Role == (int)UserRole.Client)
        {
            var client = new Client()
            {
                Name = model.Username
                // convert img from string to bytes and save it in freelancer
            };

            _clientService.Add(client);   // add + save inside the same method

            user.UserName = model.Username;

            user.Email = model.Email;

            user.ClientId = client.Id;

            var result = await _unitOfWork.ApplicationUserRepository.InsertAsync(user, "Client", model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new AuthModel { Message = errors };
            }
        }



        // Send a welcome notification to the user
        await SendWelcomeNotificationAsync(user);

        // var jwtSecurityToken = await CreateJwtToken(user);

        // Determine the user's roles
        var roles = await _userManager.GetRolesAsync(user);
        //var refreshToken = GenerateRefreshToken();
        // user.RefreshTokens?.Add(refreshToken);
        await _userManager.UpdateAsync(user);

        return new AuthModel
        {
            Email = user.Email,
            // ExpiresOn = jwtSecurityToken.ValidTo,
            IsAuthenticated = true,
            Roles = roles.ToList(),
            //Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Username = user.UserName,
            //RefreshToken = refreshToken.Token,
            //RefreshTokenExpiration = refreshToken.ExpiresOn,
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
            authModel.Message = "كلمة المرور او البريد الالكتروني غير صحيح";

            return authModel;
        }
        var jwtSecurityToken = await CreateJwtToken(user);

        var roleList = await _userManager.GetRolesAsync(user);

        if (user.ClientId != null)
        {
            authModel.Id = (int)user.ClientId;
        }
        else if (user.FreelancerId != null)
        {
            authModel.Id = (int)user.FreelancerId;
        }

        authModel.IsAuthenticated = true;
        authModel.Token =
            new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        authModel.Email = user.Email;
        authModel.Username = user.UserName;
        authModel.ExpiresOn = jwtSecurityToken.ValidTo;
        authModel.Roles = roleList.ToList();
        // authModel.Username = user?.UserName;

        if (user.RefreshTokens.Any(t => t.IsActive))
        {
            var activeRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);

            authModel.RefreshToken = activeRefreshToken.Token;
            authModel.RefreshTokenExpiration = activeRefreshToken.ExpiresOn;

        }
        else
        {
            var refreshToken = GenerateRefreshToken();

            authModel.RefreshToken = refreshToken.Token;
            authModel.RefreshTokenExpiration = refreshToken.ExpiresOn;

            user.RefreshTokens.Add(refreshToken);

            await _userManager.UpdateAsync(user);
        }

        return authModel;
    }

    public async Task<AuthModel> RefreshTokenAsync(string token)
    {
        var authModel = new AuthModel();

        var user = await _userManager
               .Users
                .SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

        if (user == null)
        {
            authModel.Message = "Invalid Token";
            return authModel;
        }

        var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

        if (!refreshToken.IsActive)
        {
            authModel.Message = "Inactive Token";
            return authModel;
        }
        refreshToken.RevokedOn = DateTime.UtcNow;

        var newRefreshToken = GenerateRefreshToken();
        user.RefreshTokens.Add(newRefreshToken);
        await _userManager.UpdateAsync(user);

        var jwtToken = await CreateJwtToken(user);

        authModel.IsAuthenticated = true;
        authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        authModel.Email = user.Email;
        authModel.Username = user.UserName;

        var roles = await _userManager.GetRolesAsync(user);

        authModel.Roles = roles.ToList();
        authModel.RefreshToken = newRefreshToken.Token;
        authModel.RefreshTokenExpiration = newRefreshToken.ExpiresOn;

        return authModel;
    }

    public async Task<bool> RevokeTokenAsync(string token)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

        if (user == null)
            return false;

        var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

        if (!refreshToken.IsActive)
            return false;

        refreshToken.RevokedOn = DateTime.UtcNow;

        await _userManager.UpdateAsync(user);

        return true;
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

    private RefreshToken GenerateRefreshToken()
    {
        var randomNumber = new byte[32];

        using var generator = new RNGCryptoServiceProvider();

        generator.GetBytes(randomNumber);

        return new RefreshToken
        {
            Token = Convert.ToBase64String(randomNumber),
            ExpiresOn = DateTime.UtcNow.AddDays(60),
            CreatedOn = DateTime.UtcNow,
        };
    }

    //Google Authentication
    public async Task<ApplicationUser> GetByIdAsync(string id)
    {
        return await _unitOfWork.ApplicationUserRepository.GetByIdAsync(id);
    }

    public async Task<ApplicationUser> GetByEmailAsync(string email)
    {
        return await _unitOfWork.ApplicationUserRepository.GetByEmailAsync(email);
    }

    public async Task<GeneralResponse> GoogleAuthenticationAsync(GoogleSignupDTO googleSignupDto)
    {
        var User = await _unitOfWork.ApplicationUserRepository
                                       .GetByEmailAsync(googleSignupDto.Email);

        if (User == null)
        {
            //// apply login logic here
            //return await Task.FromResult(new GeneralResponse()
            //{
            //    IsSuccess = false,
            //    Data = null,
            //    Message = "This email has already been registered before"
            //});

            if (googleSignupDto.Role == (int)UserRole.Freelancer)
            {
                var freelancer = new Freelancer() // consider it is a freelancer for testing
                {
                    Name = googleSignupDto.FirstName,

                    // convert img from string to bytes and save it in freelancer
                };


                try
                {
                    _freelancerService.Add(freelancer); // add + save inside the same method

                }

                catch (Exception ex)
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Data = null,
                        Message = ex.Message
                    };

                }

                User = new ApplicationUser()
                {
                    UserName = googleSignupDto.FirstName, // should add guid as suffix as gmail allow username duplication but identity user doesnot
                    Email = googleSignupDto.Email,
                    FreelancerId = freelancer.Id,
                    EmailConfirmed = true, // as he registered using gmail
                };

                try
                {
                    // should add role 
                    await _unitOfWork.ApplicationUserRepository.InsertAsync(User, "Freelancer");
                }
                catch (Exception ex)
                {
                    return await Task.FromResult(new GeneralResponse()
                    {
                        IsSuccess = false,
                        Data = ex.Message,
                        Message = "Error on account creation"
                    });
                }
            }

            else if (googleSignupDto.Role == (int)UserRole.Client)
            {
                var client = new Client()
                {
                    Name = googleSignupDto.FirstName,
                    // convert img from string to bytes and save it in client
                };

                try
                {
                    _clientService.Add(client); // add + save inside the same method

                }

                catch (Exception ex)
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Data = null,
                        Message = ex.Message
                    };

                }

                User = new ApplicationUser()
                {
                    UserName = googleSignupDto.FirstName, // should add guid as suffix as gmail allow username duplication but identity user doesnot
                    Email = googleSignupDto.Email,
                    ClientId = client.Id,
                    EmailConfirmed = true, // as he registered using gmail
                };
            }


            try
            {
                // should add role 
                await _unitOfWork.ApplicationUserRepository.InsertAsync(User, "Client");
            }

            catch (Exception ex)
            {
                return await Task.FromResult(new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = ex.Message,
                    Message = "Error on account creation"
                });
            }

            try
            {
                await SendWelcomeNotificationAsync(User);
            }

            catch (Exception ex)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = ex.Message,
                    Message = "Error on sending welcome notification"
                };
            }
        }

        //Logic for Login
        var authModel = new AuthModel();

        if (User.ClientId != null)
        {
            authModel.Id = (int)User.ClientId;
        }

        else if (User.FreelancerId != null)
        {
            authModel.Id = (int)User.FreelancerId;
        }

        var jwtSecurityToken = await CreateJwtToken(User);
        var roleList = await _userManager.GetRolesAsync(User);

        //Refresh Token 

        var refreshToken = GenerateRefreshToken();

        User.RefreshTokens.Add(refreshToken);

        authModel.IsAuthenticated = true;
        authModel.Token =
            new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        authModel.Email = User.Email;
        authModel.Username = User.UserName;
        authModel.ExpiresOn = jwtSecurityToken.ValidTo;
        authModel.Roles = roleList.ToList();
        authModel.RefreshToken = refreshToken.Token;
        authModel.RefreshTokenExpiration = refreshToken.ExpiresOn;

        return new GeneralResponse()
        {
            IsSuccess = true,
            Data = authModel,
            Message = "Successfully authenticated using gmail"
        };
    }


    public async Task<GeneralResponse> IsGmailTokenValidAsync(string GmailToken)
    {
        ValidationSettings settings = new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = new string[] { _googleAuthConfig.ClientId }
        };

        var payload = new Payload();

        try
        {
            payload = await GoogleJsonWebSignature.ValidateAsync(GmailToken, settings);
            // validate that aud of token matches clienId of my project on google cloud api   
        }

        catch (Exception ex)
        {
            return new GeneralResponse
            {
                IsSuccess = false,
                Data = null,
                Message = "Invalid gmail token"
            };
        }

        if (payload == null)
        {
            return new GeneralResponse
            {
                IsSuccess = false,
                Data = null,
                Message = "Invalid gmail token"
            };
        }

        return new GeneralResponse()
        {
            IsSuccess = true,
            Data = payload,
            Message = "Valid gmail token"
        };
    }


    // Methods for handling password reset
    public async Task<AuthModel> ForgotPasswordAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return new AuthModel { Message = "User Not found" };
        }

        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        user.PasswordResetToken = resetToken;

        //var jwtToken = await CreateJwtToken(user);
        //user.PasswordResetToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        user.ResetTokenExpires = DateTime.UtcNow.AddHours(1); // Token valid for 1 hour

        await _userManager.UpdateAsync(user);

        return new AuthModel
        {
            Message = "You may now reset your Password.",
            IsAuthenticated = true

        };
    }

    public async Task<AuthModel> ResetPasswordAsync(ResetPasswordRequest request)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);

        if (user is null)
        {
            return new AuthModel { Message = "User Not Found." };

        }

        var resetPassResult = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

        if (!resetPassResult.Succeeded)
        {
            var errors = string.Join(", ", resetPassResult.Errors.Select(e => e.Description));

            return new AuthModel { Message = errors };
        }

        user.PasswordResetToken = null;
        user.ResetTokenExpires = null;

        await _userManager.UpdateAsync(user);

        return new AuthModel
        {
            Message = "Password Successfully Reset.",
            IsAuthenticated = true,
            Token = user.PasswordResetToken
        };
    }
}
