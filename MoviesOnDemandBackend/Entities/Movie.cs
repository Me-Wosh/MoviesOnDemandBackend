namespace MoviesOnDemandBackend.Entities;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Genre { get; set; }
    public string Year { get; set; }
    public decimal Rating { get; set; }
    public int SumOfRatings { get; set; }
    public int NumberOfRatings { get; set; }
}