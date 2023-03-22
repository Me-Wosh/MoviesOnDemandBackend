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
    
    [HttpPatch("ChangeEmail")]
    public ActionResult<string> ChangeEmail([FromBody] ChangeEmailDto changeEmailDto)
    {
        var token = _accountService.ChangeEmail(changeEmailDto);

        return Ok(token);
    }
    
    [HttpPatch("ChangeUsername")]
    public ActionResult<string> ChangeUsername([FromBody] ChangeUsernameDto changeUsernameDto)
    {
        var token = _accountService.ChangeUsername(changeUsernameDto);

        return Ok(token);
    }

    [HttpPatch("ChangePassword")]
    public ActionResult ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        _accountService.ChangePassword(changePasswordDto);

        return Ok("Password successfully changed");
    }

    [HttpDelete("DeleteAccount")]
    public ActionResult DeleteUser()
    {
        _accountService.DeleteAccount();

        return NoContent();
    }
}