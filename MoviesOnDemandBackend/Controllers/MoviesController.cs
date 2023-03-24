using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesOnDemandBackend.Entities;
using MoviesOnDemandBackend.Models;
using MoviesOnDemandBackend.Services;

namespace MoviesOnDemandBackend.Controllers;

[ApiController]
[Authorize(Roles = "admin")]
[Route("Api/[Controller]/V1")]
public class MoviesController : ControllerBase
{
    private readonly IMoviesService _moviesService;

    public MoviesController(IMoviesService moviesService)
    {
        _moviesService = moviesService;
    }

    [HttpGet, AllowAnonymous]
    public ActionResult<IEnumerable<MovieDto>> GetAllMovies()
    {
        var movies = _moviesService.GetAllMovies();

        return Ok(movies);
    }

    [HttpGet("{id}"), AllowAnonymous]
    public ActionResult<MovieDto> GetMovieById([FromRoute]int id)
    {
        var movie = _moviesService.GetMovieById(id);

        return Ok(movie);
    }

    [HttpPost("AddMovie")]
    public ActionResult<MovieDto> AddMovie([FromBody] AddMovieDto addMovieDto)
    {
        var movieDto = _moviesService.AddMovie(addMovieDto);

        return Created($"/Api/Movies/V1/{movieDto.Id}", movieDto);
    }

    [HttpPatch("UpdateMovie/{id}")]
    public ActionResult UpdateMovie([FromRoute] int id, [FromBody] UpdateMovieDto updateMovieDto)
    {
        _moviesService.UpdateMovie(id, updateMovieDto);

        return Ok("Movie successfully updated");
    }

    [HttpDelete("DeleteMovie/{id}")]
    public ActionResult DeleteMovie([FromRoute] int id)
    {
        _moviesService.DeleteMovie(id);

        return NoContent();
    }
}