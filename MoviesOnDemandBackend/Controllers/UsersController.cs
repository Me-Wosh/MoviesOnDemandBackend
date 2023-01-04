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
}