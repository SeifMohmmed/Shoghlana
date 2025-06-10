using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.Core.DTOs;
public class GoogleSignupDTO
{
    public string Id { get; set; }

    public string Email { get; set; }

    public string IdToken { get; set; }

    //  public string Name { get; set; } 

    public string? PhotoUrl { get; set; }

    public string FirstName { get; set; }

}
