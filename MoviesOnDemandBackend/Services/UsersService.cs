using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using MoviesOnDemandBackend.Entities;
using MoviesOnDemandBackend.Exceptions;
using MoviesOnDemandBackend.Models;

namespace MoviesOnDemandBackend.Services;

public interface IUsersService
{
    User Register(UserRegisterDto userRegisterDto);
    string Login(UserLoginDto userLoginDto);
    UserDto LikeMovie(int id);
    UserDto DislikeMovie(int id);
}

public class UsersService : IUsersService
{
    private readonly IConfiguration _configuration;
    private readonly MoviesOnDemandDbContext _dbContext;
    private readonly IMapper _mapper;
    private static readonly User User = new User();

    public UsersService(IConfiguration configuration, MoviesOnDemandDbContext dbContext, IMapper mapper)
    {
        _configuration = configuration;
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public User Register(UserRegisterDto userRegisterDto)
    {
        CreatePasswordHash(userRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
        User.Email = userRegisterDto.Email;
        User.Username = userRegisterDto.Username;
        User.PasswordHash = passwordHash;
        User.PasswordSalt = passwordSalt;
        User.Role = "user";

        return User;
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
    
    public UserDto LikeMovie(int id)
    {
        var dbMovie = _dbContext.Movies.FirstOrDefault(m => m.Id == id);
        UserDto user;

        if (dbMovie is null)
        {
            throw new NotFoundException("Movie does not exist");
        }
        
        if (User.FavoriteMovies is null)
        {
            User.FavoriteMovies = new Collection<Movie>();
            User.FavoriteMovies.Add(dbMovie);

            user = _mapper.Map<User, UserDto>(User);

            return user;
        }

        if (User.FavoriteMovies.Contains(User.FavoriteMovies.FirstOrDefault(fav => fav.Id == dbMovie.Id)))
        {
             user = _mapper.Map<User, UserDto>(User);
             return user;
        }

        User.FavoriteMovies.Add(dbMovie);

        user = _mapper.Map<User, UserDto>(User);

        return user;
    }

    public UserDto DislikeMovie(int id)
    {
        var movie = User.FavoriteMovies.FirstOrDefault(m => m.Id == id);

        if (movie is null)
        { 
            throw new NotFoundException("User hasn't liked such movie");
        }
        
        User.FavoriteMovies.Remove(movie);

        UserDto user = _mapper.Map<User, UserDto>(User);

        return user;
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