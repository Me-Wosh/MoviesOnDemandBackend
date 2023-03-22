using System.ComponentModel.DataAnnotations;

namespace MoviesOnDemandBackend.Models;

public class ChangeUsernameDto
{
    [Required]
    public string Username { get; set; }
}