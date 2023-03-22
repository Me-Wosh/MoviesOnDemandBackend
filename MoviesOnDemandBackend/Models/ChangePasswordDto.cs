using System.ComponentModel.DataAnnotations;

namespace MoviesOnDemandBackend.Models;

public class ChangePasswordDto
{
    [Required]
    public string Password { get; set; }
    [Required]
    [MinLength(6)]
    public string NewPassword { get; set; }
    [Required]
    [MinLength(6)]
    [Compare("NewPassword")]
    public string ConfirmPassword { get; set; }
}