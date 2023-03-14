using Microsoft.AspNetCore.Mvc;
using MoviesOnDemandBackend.Models;
using MoviesOnDemandBackend.Services;

namespace MoviesOnDemandBackend.Controllers;

[ApiController]
[Route("Api/[Controller]/V1")]
public class RegistrationController : ControllerBase
{
    private readonly IRegistrationService _registrationService;

    public RegistrationController(IRegistrationService registrationService)
    {
        _registrationService = registrationService;
    }
    
    [HttpPost("Register")]
    public ActionResult<UserDto> Register([FromBody] UserRegisterDto userRegisterDto)
    {
        var userDto = _registrationService.Register(userRegisterDto);

        return Created("Api/Account/V1/Me", userDto);
    }
}