using Microsoft.EntityFrameworkCore;

namespace MoviesOnDemandBackend.Entities;

public class MoviesOnDemandDbContext : DbContext
{
    public MoviesOnDemandDbContext(DbContextOptions<MoviesOnDemandDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Movie> Movies { get; set; }
}