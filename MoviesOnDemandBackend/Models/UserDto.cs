namespace MoviesOnDemandBackend.Models;

public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public List<MovieDto> FavoriteMovies { get; set; }
}