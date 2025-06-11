using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.API.Services.Interfaces;
public interface IMailService
{
    Task SendEmailAsync(string mailTo, string subject, string body);

}
