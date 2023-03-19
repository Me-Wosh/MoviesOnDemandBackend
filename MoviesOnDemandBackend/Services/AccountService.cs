using System.Security.Claims;
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
}

public class AccountService : IAccountService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;
    private readonly MoviesOnDemandDbContext _dbContext;
    
    public AccountService(IHttpContextAccessor httpContextAccessor, IMapper mapper, MoviesOnDemandDbContext dbContext)
    {
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
}