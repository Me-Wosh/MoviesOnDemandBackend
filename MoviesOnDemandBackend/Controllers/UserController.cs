using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesOnDemandBackend.Models;
using MoviesOnDemandBackend.Services;

namespace MoviesOnDemandBackend.Controllers;

[ApiController]
[Authorize(Roles = "user")]
[Route("Api/[Controller]/V1")]
public class UserController : ControllerBase
{
    private readonly IUserService _usersService;

    public UserController(IUserService usersService)
    {
        _usersService = usersService;
    }

    [HttpPost("LikeMovie/{movieId}")]
    public ActionResult<string> LikeMovie([FromRoute] int movieId)
    {
        var status = _usersService.LikeMovie(movieId);
        
        return Ok(status);
    }

    [HttpDelete("DislikeMovie/{movieId}")]
    public ActionResult<string> DislikeMovie([FromRoute] int movieId)
    {
        var status = _usersService.DislikeMovie(movieId);
        
        return Ok(status);
    }

    [HttpPost("RateMovie")]
    public ActionResult<string> RateMovie([FromBody] MovieRating rating)
    {
        var status = _usersService.RateMovie(rating);

        return Ok(status);
    }
}