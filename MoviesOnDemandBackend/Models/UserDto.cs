namespace MoviesOnDemandBackend.Models;

public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Role { get; set; }
    public DateTime AccountCreated { get; set; }
    public HashSet<MovieDto> FavoriteMovies { get; set; }
}