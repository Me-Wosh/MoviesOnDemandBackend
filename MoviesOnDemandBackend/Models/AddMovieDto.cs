using System.ComponentModel.DataAnnotations;

namespace MoviesOnDemandBackend.Models;

public class AddMovieDto
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }
    [MaxLength(50)]
    public string? Genre { get; set; }
    [Range(0, 9999)] 
    public ushort? Year { get; set; } = null;
}