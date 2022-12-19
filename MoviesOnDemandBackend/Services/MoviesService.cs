using AutoMapper;
using MoviesOnDemandBackend.Entities;
using MoviesOnDemandBackend.Exceptions;
using MoviesOnDemandBackend.Models;

namespace MoviesOnDemandBackend.Services;

public interface IMoviesService
{
    List<MovieDto> GetAllMovies();
    MovieDto GetMovieById(int id);
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

    public List<MovieDto> GetAllMovies()
    {
        var movies = _dbContext.Movies.ToList();

        var movieDtos = _mapper.Map<List<Movie>, List<MovieDto>>(movies); 
        
        return movieDtos;
    }

    public MovieDto GetMovieById(int id)
    {
        var movie = _dbContext.Movies.FirstOrDefault(m => m.Id == id);

        if (movie is null)
            throw new NotFoundException($"Movie of given id: {id} not found.");

        var movieDto = _mapper.Map<Movie, MovieDto>(movie); 
        
        return movieDto;
    }
}