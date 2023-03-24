using System.ComponentModel.DataAnnotations;

namespace MoviesOnDemandBackend.Models;

public class ChangeEmailDto
{
    [Required]
    [EmailAddress]
    [MaxLength(60)]
    public string Email { get; set; }
}