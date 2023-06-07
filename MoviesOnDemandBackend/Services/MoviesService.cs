using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoviesOnDemandBackend.Entities;
using MoviesOnDemandBackend.Exceptions;
using MoviesOnDemandBackend.Models;

namespace MoviesOnDemandBackend.Services;

public interface IMoviesService
{
    IEnumerable<MovieDto> GetAllMovies();
    MovieDto GetMovieById(int id);
    MovieDto AddMovie(AddMovieDto addMovieDto);
    void UpdateMovie(int id, UpdateMovieDto updateMovieDto);
    void DeleteMovie(int id);
}

public class MoviesService : IMoviesService
{
    private readonly IMapper _mapper;
    private readonly MoviesOnDemandDbContext _dbContext;
    
    public MoviesService(IMapper mapper, MoviesOnDemandDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public IEnumerable<MovieDto> GetAllMovies()
    {
        var movies = _dbContext
            .Movies
            .Include(m => m.Ratings)
            .ToList();

        var movieDtos = _mapper.Map<List<Movie>, List<MovieDto>>(movies);

        for (var i = 0; i < movieDtos.Count; i++)
        {
            if (movies[i].Ratings.Count > 0)
            {
                movieDtos[i].Rating = decimal.Round(Convert.ToDecimal(movies[i].Ratings.Sum(r => r.Value))
                                                    / Convert.ToDecimal(movies[i].Ratings.Count), 1);
            }
        }
        
        return movieDtos;
    }

    public MovieDto GetMovieById(int id)
    {
        var movie = _dbContext
            .Movies
            .Include(m => m.Ratings)
            .SingleOrDefault(m => m.Id == id);

        if (movie is null)
            throw new NotFoundException($"Movie of given id: {id} not found.");

        var movieDto = _mapper.Map<Movie, MovieDto>(movie);

        if (movie.Ratings.Count > 0)
        {
            movieDto.Rating = decimal.Round(Convert.ToDecimal(movie.Ratings.Sum(r => r.Value)) 
                                            / Convert.ToDecimal(movie.Ratings.Count), 1);
        }
        
        return movieDto;
    }
    
    public MovieDto AddMovie(AddMovieDto addMovieDto)
    {
        var dbMovie = _dbContext
            .Movies
            .SingleOrDefault(m => m.Title.Replace(" ", "").ToLower() 
                                  == addMovieDto.Title.Replace(" ", "").ToLower());

        if (dbMovie is not null)
        {
            throw new BadRequestException("Movie already exists");
        }

        var movie = _mapper.Map<AddMovieDto, Movie>(addMovieDto);

        _dbContext.Movies.Add(movie);
        _dbContext.SaveChanges();

        var movieDto = _mapper.Map<Movie, MovieDto>(movie);
        
        return movieDto;
    }

    public void UpdateMovie(int id, UpdateMovieDto updateMovieDto)
    {
        var movie = _dbContext.Movies.SingleOrDefault(m => m.Id == id);

        if (movie is null)
        {
            throw new NotFoundException("Movie not found");
        }

        if (updateMovieDto.Title is not null)
        {
            movie.Title = updateMovieDto.Title;
        }

        if (updateMovieDto.Genre is not null)
        {
            movie.Genre = updateMovieDto.Genre;
        }

        if (updateMovieDto.Year is not null)
        {
            movie.Year = updateMovieDto.Year;
        }

        _dbContext.SaveChanges();
    }

    public void DeleteMovie(int id)
    {
        var movie = _dbContext.Movies.SingleOrDefault(m => m.Id == id);

        if (movie is null)
        {
            throw new NotFoundException("Movie not found");
        }

        _dbContext.Movies.Remove(movie);
        _dbContext.SaveChanges();
    }
}