using System.ComponentModel.DataAnnotations;

namespace Shoghlana.Domain.Entities;
public class ResetPasswordRequest
{
    [Required]
    public string Token { get; set; } = string.Empty;


    [Required, MinLength(6, ErrorMessage = "Please enter at least 6 characters!")]
    public string Password { get; set; }


    [Required, Compare("Password")]
    public string ConfirmPassword { get; set; }


}
