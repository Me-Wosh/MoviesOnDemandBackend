using Microsoft.AspNetCore.Mvc;
using MoviesOnDemandBackend.Models;
using MoviesOnDemandBackend.Services;

namespace MoviesOnDemandBackend.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMoviesService _moviesService;

    public MoviesController(IMoviesService moviesService)
    {
        _moviesService = moviesService;
    }

    [HttpGet]
    public ActionResult<List<MovieDto>> GetAllMovies()
    {
        var movies = _moviesService.GetAllMovies();

        return Ok(movies);
    }

    [HttpGet("{id}")]
    public ActionResult<MovieDto> GetMovieById([FromRoute]int id)
    {
        var movie = _moviesService.GetMovieById(id);

        return Ok(movie);
    }
}