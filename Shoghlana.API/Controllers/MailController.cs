using MailKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shoghlana.API.Response;
using Shoghlana.API.Services.Interfaces;
using Shoghlana.Core.Models;
using IMailService = Shoghlana.API.Services.Interfaces.IMailService;

namespace Shoghlana.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MailController : ControllerBase
{
    private readonly IMailService _mailService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAuthService _authService;

    public MailController(IMailService mailService,
        UserManager<ApplicationUser> userManager, IAuthService authService)
    {
        _mailService = mailService;
        _userManager = userManager;
        _authService = authService;
    }

    [HttpPost("SendConfirmtionEmail")]
    public async Task<GeneralResponse> SendConfirmationEmail(string tomail)
    {
        var user = await _userManager.FindByEmailAsync(tomail);

        if (user == null || string.IsNullOrEmpty(user.Email))
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Data = ModelState,
                Message = "Invalid Mail Address or there is no user"
            };
        }
        else
        {
            var token = await
                _userManager.GenerateEmailConfirmationTokenAsync(user);

            string confirmationLink = Url.Action("ConfirmEmail",
                "Mail", new { userEmail = user.Email, token }, Request.Scheme);

            string subject = "Email Confirmation";
            string body = $"<h1>Welcome to Shoghlana!</h1>" +
                              $"<p>Please confirm your email by clicking on the link below:</p>" +
                              $"<a href='{confirmationLink}'>Confirm Email</a>";

            await _mailService.SendEmailAsync(user.Email, subject, body);

            return new GeneralResponse()
            {
                IsSuccess = true,
                Data = ModelState,
                Status = 200,
                Message = "Mail Sent"
            };
        }
    }

    [HttpGet("ConfirmEmail")]
    public async Task<GeneralResponse> ConfirmEmail(string userEmail,string token)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);

        if (user == null || string.IsNullOrEmpty(user.Email))
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Data = ModelState,
                Message = "Invalid Mail Address"
            };
        }

        var result = await _userManager.ConfirmEmailAsync(user, token);

        var jwtSecurityToken = await _authService.CreateJwtToken(user);

        if (result.Succeeded)
        {
            return new GeneralResponse()
            {
                IsSuccess= true,
                Data = user.Email,
                Status=200,
                Message="Email Confirmend Successfully",
                Token=jwtSecurityToken.ToString()
            };
        }

        else
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Data = result.Errors,
                Status = 2400,
                Message = "Error confirming email"
            };
        }

    }

}
