using System.ComponentModel.DataAnnotations;

namespace MoviesOnDemandBackend.Models;

public class ChangeUsernameDto
{
    [Required]
    [MaxLength(50)]
    public string Username { get; set; }
}