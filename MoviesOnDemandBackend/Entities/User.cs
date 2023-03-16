namespace MoviesOnDemandBackend.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public string Role { get; set; }
    public DateTime AccountCreated { get; set; }
    public ICollection<Movie> FavoriteMovies { get; set; }
    public ICollection<Rating> Ratings { get; set; }
    public RefreshToken RefreshToken { get; set; }
}