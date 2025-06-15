﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.Core.Models;
public class RegisterModel
{

    [Required, StringLength(50)]
    public string Username { get; set; }


    [Required, StringLength(120)]
    public string Email { get; set; }


    [Required, StringLength(256)]
    public string Password { get; set; }


    [Required, StringLength(256)]
    [Compare("Password", ErrorMessage = "Passwords don't match")]
    public string RepeatPassword { get; set; }


    [Required, StringLength(50)]
    public string PhoneNumber { get; set; }

    public int Role { get; set; }
}
