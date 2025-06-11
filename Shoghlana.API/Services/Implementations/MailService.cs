using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Shoghlana.API.Services.Interfaces;
using Shoghlana.Core.Models;

namespace Shoghlana.API.Services.Implementations;

public class MailService : IMailService
{
    private readonly MailSettings _mailSettings;

    public MailService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }

    public async Task SendEmailAsync(string mailTo, string subject, string body)
    {
        //Sender
        var email = new MimeMessage
        {
            Sender = MailboxAddress.Parse(_mailSettings.Email),
            Subject = subject,
        };

        //Reciver
        email.To.Add(MailboxAddress.Parse(mailTo));


        var builder = new BodyBuilder();

        builder.HtmlBody = body;
        email.Body = builder.ToMessageBody();
        email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Email));

        //Connect Mail Provider
        using var smtp = new SmtpClient();
        smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
        smtp.Authenticate(_mailSettings.Email, _mailSettings.Password);
        await smtp.SendAsync(email);

        smtp.Disconnect(true);

    }
}
