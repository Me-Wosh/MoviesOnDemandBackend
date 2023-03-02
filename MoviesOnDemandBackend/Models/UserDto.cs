namespace MoviesOnDemandBackend.Models;

public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Role { get; set; }
    public DateTime AccountCreated { get; set; }
    public List<MovieDto> FavoriteMovies { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenCreated { get; set; }
    public DateTime RefreshTokenExpires { get; set; }
}