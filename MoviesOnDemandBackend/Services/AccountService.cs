using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoviesOnDemandBackend.Entities;
using MoviesOnDemandBackend.Exceptions;
using MoviesOnDemandBackend.Models;

namespace MoviesOnDemandBackend.Services;

public interface IAccountService
{
    UserDto GetCurrentUser();
    IEnumerable<MovieDto> GetUserFavoriteMovies();
    string ChangeEmail(ChangeEmailDto changeEmailDto);
    string ChangeUsername(ChangeUsernameDto changeUsernameDto);
    void ChangePassword(ChangePasswordDto changePasswordDto);
    void DeleteAccount();

}

public class AccountService : IAccountService
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;
    private readonly MoviesOnDemandDbContext _dbContext;
    
    public AccountService
    (
        IAuthenticationService authenticationService, 
        IHttpContextAccessor httpContextAccessor, 
        IMapper mapper, 
        MoviesOnDemandDbContext dbContext
    )
    {
        _authenticationService = authenticationService;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
        _dbContext = dbContext;
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
            Username = httpContext.User.FindFirstValue(ClaimTypes.Name),
            Role = httpContext.User.FindFirstValue(ClaimTypes.Role),
            AccountCreated = Convert.ToDateTime(httpContext.User.FindFirstValue("AccountCreated"))
        };

        return userDto;
    }

    public IEnumerable<MovieDto> GetUserFavoriteMovies()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        
        if (httpContext is null)
        {
            throw new BadRequestException("Couldn't process request");
        }

        var movies = _dbContext
            .Users
            .Where(u => u.Id == Convert.ToInt32(httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)))
            .SelectMany(u => u.FavoriteMovies)
            .Include(m => m.Ratings)
            .ToList();

        var movieDtos = _mapper.Map<List<Movie>,List<MovieDto>>(movies);

        for (int i = 0; i < movieDtos.Count; i++)
        {
            if (movies[i].Ratings.Count == 0)
                continue;
            
            movieDtos[i].Rating = decimal.Round(Convert.ToDecimal(movies[i].Ratings.Sum(r => r.Value)) 
                                                / Convert.ToDecimal(movies[i].Ratings.Count),1);
        }
        
        return movieDtos;
    }

    public string ChangeEmail(ChangeEmailDto changeEmailDto)
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            throw new BadRequestException("Couldn't process request");
        }

        var email = _dbContext
            .Users
            .Select(u => u.Email)
            .SingleOrDefault(u => u.Equals(changeEmailDto.Email));
        
        if (email is not null)
        {
            throw new BadRequestException("E-mail already in use");
        }

        var user = _dbContext
            .Users
            .SingleOrDefault(u =>
                u.Id == Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        user.Email = changeEmailDto.Email;
        _dbContext.SaveChanges();

        return _authenticationService.RefreshToken();
    }

    public string ChangeUsername(ChangeUsernameDto changeUsernameDto)
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            throw new BadRequestException("Couldn't process request");
        }

        var username = _dbContext
            .Users
            .Select(u => u.Username)
            .SingleOrDefault(u => u.Equals(changeUsernameDto.Username));

        if (username is not null)
        {
            throw new BadRequestException("Username already in use");
        }

        var user = _dbContext
            .Users
            .SingleOrDefault(u =>
                u.Id == Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }
        
        user.Username = changeUsernameDto.Username;
        _dbContext.SaveChanges();
        
        return _authenticationService.RefreshToken();
    }

    public void ChangePassword(ChangePasswordDto changePasswordDto)
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            throw new BadRequestException("Couldn't process request");
        }

        var user = _dbContext
            .Users
            .SingleOrDefault(u =>
                u.Id == Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
        
        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        if (!VerifyPasswordHash(changePasswordDto.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new BadRequestException("Wrong password");
        }

        if (VerifyPasswordHash(changePasswordDto.ConfirmPassword, user.PasswordHash, user.PasswordSalt))
        {
            throw new BadRequestException("You cannot set your current password as your new password");
        }
        
        CreatePasswordHash(changePasswordDto.ConfirmPassword, out byte[] passwordHash, out byte[] passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        _dbContext.SaveChanges();
    }

    public void DeleteAccount()
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            throw new BadRequestException("Couldn't process request");
        }

        var user = _dbContext
            .Users
            .SingleOrDefault(u => 
                u.Id == Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        _dbContext.Users.Remove(user);
        _dbContext.SaveChanges();
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
    
    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}