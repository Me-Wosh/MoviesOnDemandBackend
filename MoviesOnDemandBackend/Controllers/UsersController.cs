using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesOnDemandBackend.Models;
using MoviesOnDemandBackend.Services;

namespace MoviesOnDemandBackend.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }
    
    [HttpPost("Register")]
    public ActionResult<int> Register([FromBody] UserRegisterDto userRegisterDto)
    {
        var userId = _usersService.Register(userRegisterDto);

        return Ok(userId);
    }

    [HttpPost("Login")]
    public ActionResult<string> Login([FromBody] UserLoginDto userLoginDto)
    {
        var token = _usersService.Login(userLoginDto);

        return Ok(token);
    }

    [HttpPost("{userId}/LikeMovie/{movieId}"), Authorize(Roles = "user")]
    public ActionResult<string> LikeMovie([FromRoute] int userId, [FromRoute] int movieId)
    {
        _usersService.LikeMovie(userId, movieId);
        
        return Ok("Movie successfully liked");
    }

    [HttpDelete("{userId}/DislikeMovie/{movieId}"), Authorize(Roles = "user")]
    public ActionResult<string> DislikeMovie([FromRoute] int userId, [FromRoute] int movieId)
    {
        _usersService.DislikeMovie(userId, movieId);
        
        return Ok("Movie disliked");
    }

    [HttpPost("{id}/RefreshToken")]
    public ActionResult<string> RefreshToken([FromRoute] int id)
    {
        var token = _usersService.RefreshToken(id);
        return Ok(token);
    }
}