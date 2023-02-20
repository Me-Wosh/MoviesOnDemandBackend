namespace MoviesOnDemandBackend.Models;

public class RefreshToken
{
    public string Token { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateExpires { get; set; }
}