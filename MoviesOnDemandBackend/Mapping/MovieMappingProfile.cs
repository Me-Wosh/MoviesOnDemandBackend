using AutoMapper;
using MoviesOnDemandBackend.Entities;
using MoviesOnDemandBackend.Models;

namespace MoviesOnDemandBackend.Mapping;

public class MovieMappingProfile : Profile
{
    public MovieMappingProfile()
    {
        CreateMap<Movie, MovieDto>();
        CreateMap<AddMovieDto, Movie>();
    }
}