using System.ComponentModel.DataAnnotations;

namespace MoviesOnDemandBackend.Models;

public class MovieRating
{
    public int MovieId { get; set; }
    [Range(1,6)]
    public ushort Value { get; set; }
}