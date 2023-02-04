using AutoMapper;
using MoviesOnDemandBackend.Entities;
using MoviesOnDemandBackend.Models;

namespace MoviesOnDemandBackend.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>();
    }
}