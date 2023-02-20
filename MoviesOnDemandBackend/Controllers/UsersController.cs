using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesOnDemandBackend.Entities;
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
    public ActionResult<User> Register([FromBody] UserRegisterDto userRegisterDto)
    {
        var user = _usersService.Register(userRegisterDto);

        return Ok(user);
    }

    [HttpPost("Login")]
    public ActionResult<string> Login([FromBody] UserLoginDto userLoginDto)
    {
        var token = _usersService.Login(userLoginDto);

        return Ok(token);
    }

    [HttpPost("LikeMovie/{id}"), Authorize(Roles = "user")]
    public ActionResult<UserDto> LikeMovie([FromRoute] int id)
    {
        var user = _usersService.LikeMovie(id);
        return Ok(user);
    }

    [HttpDelete("DislikeMovie/{id}"), Authorize(Roles = "user")]
    public ActionResult<UserDto> DislikeMovie([FromRoute] int id)
    {
        var user = _usersService.DislikeMovie(id);
        return Ok(user);
    }

    [HttpPost("RefreshToken")]
    public ActionResult<string> RefreshToken()
    {
        var token = _usersService.RefreshToken();
        return Ok(token);
    }
}