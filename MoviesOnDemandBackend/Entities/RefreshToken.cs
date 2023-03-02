namespace MoviesOnDemandBackend.Entities;

public class RefreshToken
{
    public int Id { get; set; }
    public string Token { get; set; }
    public DateTime Created { get; set; }
    public DateTime Expires { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}