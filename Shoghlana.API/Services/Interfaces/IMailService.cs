namespace Shoghlana.API.Services.Interfaces;
public interface IMailService
{
    Task SendEmailAsync(string mailTo, string subject, string body);

}
