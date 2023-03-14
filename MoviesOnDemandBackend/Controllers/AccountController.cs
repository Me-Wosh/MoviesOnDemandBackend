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
    private readonly IAccountManagingService _accountManagingService;

    public AccountController(IAccountManagingService accountManagingService)
    {
        _accountManagingService = accountManagingService;
    }

    [HttpGet("Me")]
    public ActionResult<UserDto> GetCurrentUser()
    {
        var userDto = _accountManagingService.GetCurrentUser();

        return userDto;
    }
}