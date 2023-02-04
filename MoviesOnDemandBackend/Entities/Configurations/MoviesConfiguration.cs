using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MoviesOnDemandBackend.Entities.Configurations;

public class MoviesConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("Movies");
        
        builder
            .Property(m => m.Id)
            .IsRequired();

        builder
            .Property(m => m.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(m => m.Genre)
            .HasMaxLength(50);

        builder
            .Property(m => m.Year)
            .HasMaxLength(4)
            .IsRequired();

        builder
            .Property(m => m.Rating)
            .HasPrecision(2,1)
            .HasDefaultValue(0)
            .IsRequired();

        builder
            .Property(m => m.SumOfRatings)
            .HasDefaultValue(0)
            .IsRequired();

        builder
            .Property(m => m.NumberOfRatings)
            .HasDefaultValue(0)
            .IsRequired();
        
        builder
            .HasData(
                new Movie
                {
                    Id = 1,
                    Title = "The Godfather",
                    Genre = "Crime",
                    Year = 1972
                },
                
                new Movie
                {
                    Id = 2,
                    Title = "The Dark Knight",
                    Genre = "Action",
                    Year = 2008
                },
                
                new Movie
                {
                    Id = 3,
                    Title = "Pulp Fiction",
                    Genre = "Crime",
                    Year = 1994                    
                },
                
                new Movie
                {
                    Id = 4,
                    Title = "Fight Club",
                    Genre = "Drama",
                    Year = 1999
                },
        
                new Movie
                {
                    Id = 5,
                    Title = "The Matrix",
                    Genre = "Sci-Fi",
                    Year = 1999
                },
                
                new Movie
                {
                    Id = 6,
                    Title = "Life is Beautiful",
                    Genre = "Comedy",
                    Year = 1997
                },
                
                new Movie
                {
                    Id = 7,
                    Title = "Spirited Away",
                    Genre = "Animation",
                    Year = 2001
                },
                
                new Movie
                {
                    Id = 8,
                    Title = "Psycho",
                    Genre = "Horror",
                    Year = 1960
                },
        
                new Movie
                {
                    Id = 9,
                    Title = "Django Unchained",
                    Genre = "Western",
                    Year = 2012
                },
        
                new Movie
                {
                    Id = 10,
                    Title = "Top Gun: Maverick",
                    Genre = "Action",
                    Year = 2022
                }
            );
    }
}