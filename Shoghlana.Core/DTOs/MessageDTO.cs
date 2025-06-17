using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.Core.DTOs;
public class MessageDTO
{
    [Required]
    public string From { get; set; }

    public string To { get; set; }

    [Required]
    public string Content { get; set; }

}
