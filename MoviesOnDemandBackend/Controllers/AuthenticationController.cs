using Microsoft.AspNetCore.Mvc;
using MoviesOnDemandBackend.Models;
using MoviesOnDemandBackend.Services;

namespace MoviesOnDemandBackend.Controllers;

[ApiController]
[Route("/Api/[Controller]/V1")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    
    [HttpPost("Login")]
    public ActionResult<string> Login([FromBody] UserLoginDto userLoginDto)
    {
        var token = _authenticationService.Login(userLoginDto);

        return Ok(token);
    }
    
    [HttpPost("RefreshToken")]
    public ActionResult<string> RefreshToken()
    {
        var token = _authenticationService.RefreshToken();
        
        return Ok(token);
    }
}