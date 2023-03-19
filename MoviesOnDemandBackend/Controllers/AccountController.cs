using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesOnDemandBackend.Models;
using MoviesOnDemandBackend.Services;

namespace MoviesOnDemandBackend.Controllers;

[ApiController]
[Authorize(Roles = "user")]
[Route("/Api/[Controller]/V1")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet("Me")]
    public ActionResult<UserDto> GetCurrentUser()
    {
        var userDto = _accountService.GetCurrentUser();

        return Ok(userDto);
    }

    [HttpGet("FavoriteMovies")]
    public ActionResult<IEnumerable<MovieDto>> GetFavoriteMovies()
    {
        var movieDtos = _accountService.GetUserFavoriteMovies();

        return Ok(movieDtos);
    }
}