using System.Security.Claims;
using MoviesOnDemandBackend.Exceptions;
using MoviesOnDemandBackend.Models;

namespace MoviesOnDemandBackend.Services;

public interface IAccountManagingService
{
    UserDto GetCurrentUser();
}

public class AccountManagingService : IAccountManagingService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountManagingService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public UserDto GetCurrentUser()
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext is null)
        {
            throw new BadRequestException("Couldn't process request");
        }

        UserDto userDto = new UserDto
        {
            Id = Convert.ToInt32(httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)),
            Email = httpContext.User.FindFirstValue(ClaimTypes.Email),
            Username = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier),
            Role = httpContext.User.FindFirstValue(ClaimTypes.Role),
            AccountCreated = Convert.ToDateTime(httpContext.User.FindFirstValue("AccountCreated"))
        };

        return userDto;
    }
}