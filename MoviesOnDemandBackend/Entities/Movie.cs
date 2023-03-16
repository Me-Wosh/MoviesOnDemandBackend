namespace MoviesOnDemandBackend.Entities;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Genre { get; set; }
    public ushort Year { get; set; }
    
    public ICollection<User> Users { get; set; }
    public ICollection<Rating> Ratings { get; set; }
}