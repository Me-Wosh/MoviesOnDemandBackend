using System.ComponentModel.DataAnnotations;

namespace MoviesOnDemandBackend.Models;

public class UserRegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Username { get; set; }
    [MinLength(6)]
    public string Password { get; set; }
}