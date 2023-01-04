using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MoviesOnDemandBackend.Entities;
using MoviesOnDemandBackend.Exceptions;
using MoviesOnDemandBackend.Models;

namespace MoviesOnDemandBackend.Services;

public interface IUsersService
{
    User Register(UserRegisterDto userRegisterDto);
    string Login(UserLoginDto userLoginDto);
}

public class UsersService : IUsersService
{
    private readonly IConfiguration _configuration;
    private static readonly User User = new User();
    
    public UsersService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public User Register(UserRegisterDto userRegisterDto)
    {
        CreatePasswordHash(userRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
        User.Email = userRegisterDto.Email;
        User.Username = userRegisterDto.Username;
        User.PasswordHash = passwordHash;
        User.PasswordSalt = passwordSalt;
        User.Role = "user";

        return (User);
    }

    public string Login(UserLoginDto userLoginDto)
    {
        if (!userLoginDto.Username.Equals(User.Username))
            throw new NotFoundException("User not found");

        if (!VerifyPasswordHash(userLoginDto.Password, User.PasswordHash, User.PasswordSalt))
            throw new BadRequestException("Wrong password");

        string token = CreateToken(User);
        
        return token;
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
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
            );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        
        return jwt;
    }
}