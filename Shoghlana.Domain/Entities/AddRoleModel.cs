using System.ComponentModel.DataAnnotations;

namespace Shoghlana.Domain.Entities;
public class AddRoleModel
{
    [Required]
    public string UserId { get; set; }

    [Required]
    public string Role { get; set; }

}
