using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using MoviesOnDemandBackend.Entities;
using MoviesOnDemandBackend.Exceptions;

namespace MoviesOnDemandBackend.Services;

public interface IUserService
{
    string LikeMovie(int movieId);
    string DislikeMovie(int movieId);
}

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly MoviesOnDemandDbContext _dbContext;

    public UserService(IHttpContextAccessor httpContextAccessor, MoviesOnDemandDbContext dbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _dbContext = dbContext;
    }

    public string LikeMovie(int movieId)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext is null)
        {
            throw new BadRequestException("Couldn't process request");
        }
        
        var movie = _dbContext.Movies.SingleOrDefault(m => m.Id == movieId);

        var user = _dbContext
            .Users
            .Include(u => u.FavoriteMovies)
            .SingleOrDefault(u => u.Id == Convert.ToInt32(httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));

        if (movie is null)
        {
            throw new NotFoundException("Movie does not exist");
        }

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        if (user.FavoriteMovies.Contains(movie))
        {
            throw new BadRequestException("User has already liked given movie");
        }

        user.FavoriteMovies.Add(movie);
        _dbContext.SaveChanges();

        return "Movie successfully liked";
    }

    public string DislikeMovie(int movieId)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext is null)
        { 
            throw new BadRequestException("Couldn't process request");
        }
        
        var movie = _dbContext.Movies.SingleOrDefault(m => m.Id == movieId);
        
        var user = _dbContext
            .Users
            .Include(u => u.FavoriteMovies)
            .SingleOrDefault(u => u.Id == Convert.ToInt32(httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));

        if (movie is null)
        {
            throw new NotFoundException("Movie not found");
        }
        
        if (user is null)
        { 
            throw new NotFoundException("User not found");
        }

        if (!user.FavoriteMovies.Contains(movie))
        {
            throw new NotFoundException("User has not liked such movie");
        }

        user.FavoriteMovies.Remove(movie);
        _dbContext.SaveChanges();
        
        return "Movie successfully disliked";
    }
}