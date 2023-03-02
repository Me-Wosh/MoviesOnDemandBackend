using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MoviesOnDemandBackend.Entities;
using MoviesOnDemandBackend.Exceptions;
using MoviesOnDemandBackend.Models;

namespace MoviesOnDemandBackend.Services;

public interface IUsersService
{
    int Register(UserRegisterDto userRegisterDto);
    string Login(UserLoginDto userLoginDto);
    string RefreshToken(int id);
    void LikeMovie(int userId, int movieId);
    void DislikeMovie(int userId, int movieId);
}

public class UsersService : IUsersService
{
    private readonly IConfiguration _configuration;
    private readonly MoviesOnDemandDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public UsersService(
        IConfiguration configuration, 
        MoviesOnDemandDbContext dbContext, 
        IHttpContextAccessor httpContextAccessor, 
        IMapper mapper)
    {
        _configuration = configuration;
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }
    
    public int Register(UserRegisterDto userRegisterDto)
    {
        var dbUser = _dbContext.Users.SingleOrDefault(u => u.Email == userRegisterDto.Email);
        
        if (dbUser is not null)
        {
            throw new BadRequestException("User already exists");
        }

        dbUser = _dbContext.Users.SingleOrDefault(u => u.Username == userRegisterDto.Username);

        if (dbUser is not null)
        {
            throw new BadRequestException("Username already taken");
        }

        var user = _mapper.Map<UserRegisterDto, User>(userRegisterDto);
        user.AccountCreated = DateTime.Today;
        
        CreatePasswordHash(userRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
        
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        return user.Id;
    }
    
    public string Login(UserLoginDto userLoginDto)
    {
        var user = _dbContext.Users.SingleOrDefault(u => u.Email == userLoginDto.Email);
        
        if (user is null)
            throw new NotFoundException("User not found");

        if (!VerifyPasswordHash(userLoginDto.Password, user.PasswordHash, user.PasswordSalt))
            throw new BadRequestException("Wrong password");

        string token = CreateToken(user);

        var refreshToken = GenerateRefreshToken(user);
        SetRefreshToken(refreshToken);

        return token;
    }

    public string RefreshToken(int id)
    {
        var user = _dbContext
            .Users
            .Include(u => u.RefreshToken)
            .Single(u => u.Id == id);
        
        var refreshToken = _httpContextAccessor.HttpContext?.Request.Cookies["refreshToken"];

        if (!user.RefreshToken.Token.Equals(refreshToken))
        {
            throw new UnauthorizedException("Invalid refresh token");
        }
        
        if (user.RefreshToken.Expires < DateTime.Now)
        {
            throw new UnauthorizedException("Refresh token expired");
        }

        string token = CreateToken(user);
        var newRefreshToken = GenerateRefreshToken(user);
        SetRefreshToken(newRefreshToken);

        return token;
    }
    
    public void LikeMovie(int userId, int movieId)
    {
        var movie = _dbContext.Movies.SingleOrDefault(m => m.Id == movieId);
        
        var user = _dbContext
            .Users
            .Include(u => u.FavoriteMovies)
            .SingleOrDefault(u => u.Id == userId);
        
        var userMovies = _dbContext
            .Users
            .SelectMany(u => u.FavoriteMovies)
            .ToHashSet();

        if (movie is null)
        {
            throw new NotFoundException("Movie does not exist");
        }

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        if (userMovies.Contains(movie))
        {
            throw new BadRequestException("User has already liked given movie");
        }
        
        user.FavoriteMovies.Add(movie);
        _dbContext.SaveChanges();
    }

    public void DislikeMovie(int userId, int movieId)
    {
        var movie = _dbContext.Movies.SingleOrDefault(m => m.Id == movieId);
        
        var user = _dbContext
            .Users
            .Include(u => u.FavoriteMovies)
            .SingleOrDefault(u => u.Id == userId);
        
        var userMovies = _dbContext
            .Users
            .SelectMany(u => u.FavoriteMovies)
            .ToHashSet();

        if (movie is null)
        {
            throw new NotFoundException("Movie not found");
        }
        
        if (user is null)
        { 
            throw new NotFoundException("User not found");
        }

        if (!userMovies.Contains(movie))
        {
            throw new NotFoundException("User has not liked such movie");
        }

        user.FavoriteMovies.Remove(movie);
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

    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
            );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        
        return jwt;
    }

    private RefreshToken GenerateRefreshToken(User user)
    {
        var dbRefreshToken = _dbContext.RefreshTokens.SingleOrDefault(r => r.UserId == user.Id);
        
        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Created = DateTime.Now,
            Expires = DateTime.Now.AddDays(7),
            UserId = user.Id
        };

        if (dbRefreshToken is null)
        {
            _dbContext.RefreshTokens.Add(refreshToken);
        }
        
        else
        {
            dbRefreshToken.Token = refreshToken.Token;
            dbRefreshToken.Created = refreshToken.Created;
            dbRefreshToken.Expires = refreshToken.Expires;
        }
        
        _dbContext.SaveChanges();

        return refreshToken;
    }

    private void SetRefreshToken(RefreshToken refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = refreshToken.Expires
        };

        _httpContextAccessor.HttpContext?.Response.Cookies.Append(
            "refreshToken", 
            refreshToken.Token, 
            cookieOptions);
    }
}