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

public interface IAuthenticationService
{
    string Login(UserLoginDto userLoginDto);
    string RefreshToken();
}

public class AuthenticationService : IAuthenticationService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;
    private readonly MoviesOnDemandDbContext _dbContext;

    public AuthenticationService
    (
        IConfiguration configuration, 
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper,
        MoviesOnDemandDbContext dbContext
    )
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public string Login(UserLoginDto userLoginDto)
    {
        var user = _dbContext
            .Users
            .SingleOrDefault(u => u.Email == userLoginDto.Email);

        if (user is null)
            throw new NotFoundException("User not found");

        if (!VerifyPasswordHash(userLoginDto.Password, user.PasswordHash, user.PasswordSalt))
            throw new BadRequestException("Wrong password");

        var userDto = _mapper.Map<User, UserDto>(user);

        string token = CreateToken(userDto);

        var refreshToken = GenerateRefreshToken(userDto);
        SetRefreshToken(refreshToken);

        return token;
    }
    
    public string RefreshToken()
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            throw new BadRequestException("Couldn't process request");
        }
        
        var refreshToken = _httpContextAccessor.HttpContext.Request.Cookies["refreshToken"];
        
        var dbRefreshToken = _dbContext
            .RefreshTokens
            .SingleOrDefault(r => r.Token == refreshToken);

        if (dbRefreshToken is null)
        {
            throw new UnauthorizedException("Invalid refresh token, log in to continue");
        }
        
        if (!dbRefreshToken.Token.Equals(refreshToken))
        {
            throw new UnauthorizedException("Invalid refresh token, log in to continue");
        }
        
        if (dbRefreshToken.Expires < DateTime.Now)
        {
            throw new UnauthorizedException("Refresh token expired, log in to continue");
        }

        var user = _dbContext
            .Users
            .Include(u => u.RefreshToken)
            .Single(u => u.RefreshToken.Id == dbRefreshToken.Id);

        var userDto = _mapper.Map<User, UserDto>(user);

        string token = CreateToken(userDto);
        var newRefreshToken = GenerateRefreshToken(userDto);
        SetRefreshToken(newRefreshToken);

        return token;
    }
    
    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }

    private string CreateToken(UserDto userDto)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim("AccountCreated", userDto.AccountCreated.ToString()),
            new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
            new Claim(ClaimTypes.Email, userDto.Email),
            new Claim(ClaimTypes.Name, userDto.Username),
            new Claim(ClaimTypes.Role, userDto.Role)
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

    private RefreshToken GenerateRefreshToken(UserDto userDto)
    {
        var dbRefreshToken = _dbContext.RefreshTokens.SingleOrDefault(r => r.UserId == userDto.Id);
        
        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Created = DateTime.Now,
            Expires = DateTime.Now.AddDays(7),
            UserId = userDto.Id
        };

        if (dbRefreshToken is null)
        {
            _dbContext.RefreshTokens.Add(refreshToken);
        }
        
        if (dbRefreshToken is not null)
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