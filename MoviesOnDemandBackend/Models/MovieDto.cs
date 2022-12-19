namespace MoviesOnDemandBackend.Models;

public class MovieDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Genre { get; set; }
    public string Year { get; set; }
    public decimal Rating { get; set; }
}