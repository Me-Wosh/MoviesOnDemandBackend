using System.ComponentModel.DataAnnotations;

namespace MoviesOnDemandBackend.Models;

public class ChangeEmailDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}