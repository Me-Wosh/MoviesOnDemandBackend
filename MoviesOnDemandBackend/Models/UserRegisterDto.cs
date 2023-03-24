using System.ComponentModel.DataAnnotations;

namespace MoviesOnDemandBackend.Models;

public class UserRegisterDto
{
    [Required]
    [EmailAddress]
    [MaxLength(60)]
    public string Email { get; set; }
    [Required]
    [MaxLength(50)]
    public string Username { get; set; }
    [Required]
    [MinLength(6)]
    public string Password { get; set; }
}