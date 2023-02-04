using Microsoft.EntityFrameworkCore;
using MoviesOnDemandBackend.Entities.Configurations;

namespace MoviesOnDemandBackend.Entities;

public class MoviesOnDemandDbContext : DbContext
{
    public MoviesOnDemandDbContext(DbContextOptions<MoviesOnDemandDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Movie> Movies { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MoviesConfiguration());
        modelBuilder.ApplyConfiguration(new UsersConfiguration());
    }
}