using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using MoviesOnDemandBackend.Entities;
using MoviesOnDemandBackend.Exceptions;
using MoviesOnDemandBackend.Models;

namespace MoviesOnDemandBackend.Services;

public interface IRegistrationService
{
    UserDto Register(UserRegisterDto userRegisterDto);
}

public class RegistrationService : IRegistrationService
{
    private readonly IMapper _mapper;
    private readonly MoviesOnDemandDbContext _dbContext;

    public RegistrationService(IMapper mapper, MoviesOnDemandDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public UserDto Register(UserRegisterDto userRegisterDto)
    {
        var email = _dbContext
            .Users
            .Select(u => u.Email)
            .SingleOrDefault(e => e.Equals(userRegisterDto.Email));
        
        if (email is not null)
        {
            throw new BadRequestException("User already exists");
        }

        var username = _dbContext
            .Users
            .Select(u => u.Username)
            .SingleOrDefault(u => u.Equals(userRegisterDto.Username));

        if (username is not null)
        {
            throw new BadRequestException("Username taken");
        }

        var user = _mapper.Map<UserRegisterDto, User>(userRegisterDto);
        user.AccountCreated = DateTime.Today;
        
        CreatePasswordHash(userRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
        
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        var userDto = _mapper.Map<User, UserDto>(user);

        return userDto;
    }
    
    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}