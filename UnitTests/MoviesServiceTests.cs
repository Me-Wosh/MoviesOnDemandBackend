using Microsoft.AspNetCore.Mvc;
using Moq;
using MoviesOnDemandBackend.Controllers;
using MoviesOnDemandBackend.Entities;
using MoviesOnDemandBackend.Exceptions;
using MoviesOnDemandBackend.Models;
using MoviesOnDemandBackend.Services;

namespace UnitTests;

public class MoviesServiceTests
{
    private List<MovieDto> _moviesDtos;
    private List<Movie> _movies;
    private int _id = 1;
    private static Mock<IMoviesService> _service = new();
    private readonly MoviesController _controller;

    public MoviesServiceTests()
    {
        // arrange
        _moviesDtos = new List<MovieDto>
        {
            new MovieDto
            {
                Id = 1,
                Title = "The Godfather",
                Genre = "Crime",
                Year = 1972
            },

            new MovieDto
            {
                Id = 2,
                Title = "The Dark Knight",
                Genre = "Action",
                Year = 2008
            }
        };

        _movies = new List<Movie>
        {
            new Movie
            {
                Id = 1,
                Title = "The Godfather",
                Genre = "Crime",
                Year = 1972
            },

            new Movie
            {
                Id = 2,
                Title = "The Dark Knight",
                Genre = "Action",
                Year = 2008
            }
        };

        _service = new Mock<IMoviesService>();
        _service
            .Setup(s => s.GetAllMovies())
            .Returns(_moviesDtos);

        _service
            .Setup(s => s.GetMovieById(It.IsAny<int>()))
            .Returns<int>(id =>
            {
                if (_moviesDtos.Exists(m => m.Id == id))
                    return _moviesDtos.Single(m => m.Id == id);

                throw new NotFoundException($"Movie of given id: {id} not found.");
            });
        
        _service
            .Setup(s => s.UpdateMovie(It.IsAny<int>(), It.IsAny<UpdateMovieDto>()))
            .Callback<int, UpdateMovieDto>((id, updMovieDto) =>
            {
                if (_movies.Exists(m => m.Id == id))
                {
                    var movie = _movies.Single(m => m.Id == id);
                    movie.Title = updMovieDto.Title!;
                    movie.Genre = updMovieDto.Genre!;
                    movie.Year = updMovieDto.Year!;
                }

                else
                    throw new NotFoundException("Movie not found");
            });

        _service
            .Setup(s => s.DeleteMovie(It.IsAny<int>()))
            .Callback<int>(id =>
            {
                if (_movies.Exists(m => m.Id == id))
                    _movies.Remove(_movies.Single(m => m.Id == id));

                else
                    throw new NotFoundException("Movie not found");
            });

        _controller = new MoviesController(_service.Object);
    }
    
    [Fact]
    public void GetAllMovies_IfMoviesExist_ReturnsIEnumerableOfAllTheseMovies()
    {
        // act
        var result = (ObjectResult)_controller.GetAllMovies().Result!;
        var resultValue = (List<MovieDto>)result.Value!;
    
        // assert
        Assert.NotNull(result);
        Assert.NotEmpty(resultValue);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(_moviesDtos!.Count, resultValue.Count);
        Assert.Equal(_moviesDtos.ElementAt(0).Title, resultValue.ElementAt(0).Title);
        Assert.Equal(_moviesDtos.ElementAt(1).Title, resultValue.ElementAt(1).Title);
    }
    
    [Fact]
    public void GetAllMovies_IfMoviesDontExist_ReturnsEmptyIEnumerable()
    {
        // arrange
        _moviesDtos.Clear();
    
        // act
        var result = (ObjectResult)_controller.GetAllMovies().Result!;
        var resultValue = (List<MovieDto>)result.Value!;
        
        // assert
        Assert.Equal(200, result.StatusCode);
        Assert.Empty(resultValue);
    }
    
    [Fact]
    public void GetMovieById_GivenExistingId_ReturnsMovieDtoOfGivenId()
    {
        // act    
        var result = (ObjectResult)_controller.GetMovieById(_id).Result!;
        var resultValue = (MovieDto)result.Value!;
        
        // assert
        Assert.NotNull(result);
        Assert.NotNull(resultValue);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(_moviesDtos.First().Title, resultValue.Title);
    }
    
    [Fact]
    public void GetMovieById_GivenNonExistingId_ThrowsNotFoundException()
    {
        // arrange
        _id = -1;

        // act
        var act = () => _controller.GetMovieById(_id);
        
        // assert
        var exception = Assert.Throws<NotFoundException>(act);
        Assert.Equal($"Movie of given id: {_id} not found.", exception.Message);
    }
    
    [Theory]
    [InlineData(new int[3] {5, 4, 4}, 4.3)]
    [InlineData(new int[3] {5, 2, 3}, 3.3)]
    [InlineData(new int[] {}, 0.0)]
    public void GetAllMovies_CalculatesRatingsProperly(int[] ratings, decimal rating)
    {
        // arrange
        _movies.First().Ratings = new List<Rating>();

        if (ratings.Length > 0)
        {
            _movies.First().Ratings = new List<Rating>
            {
                new Rating
                {
                    Value = ratings.ElementAt(0)
                },

                new Rating
                {
                    Value = ratings.ElementAt(1)
                },

                new Rating
                {
                    Value = ratings.ElementAt(2)
                }
            };
        }
        
        _service
            .Setup(s => s.GetAllMovies())
            .Returns(
                new List<MovieDto>
                {
                    new MovieDto
                    {
                        Id = _movies.First().Id,
                        Title = _movies.First().Title,
                        Genre = _movies.First().Genre,
                        Year = _movies.First().Year,
                        Rating = _movies.First().Ratings.Count > 0 ? 
                            decimal.Round(Convert.ToDecimal(_movies.First().Ratings.Sum(r => r.Value))
                                          / Convert.ToDecimal(_movies.First().Ratings.Count), 1) : 0.0m
                    }
                });
        
        // act
        var result = (ObjectResult)_controller.GetAllMovies().Result!;
        var resultValue = (List<MovieDto>)result.Value!;

        // assert
        Assert.NotNull(result);
        Assert.NotEmpty(resultValue);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(rating, resultValue.First().Rating);
    }

    [Fact]
    public void AddMovie_GivenNonExistingMovie_CreatesMovieAndReturnsMovieDto()
    {
        // arrange
        _movies.Clear();

        var addMovieDto = new AddMovieDto
        {
            Title = "John Wick",
            Genre = "Action",
            Year = 2014
        };
        
        _service
            .Setup(s => s.AddMovie(It.IsAny<AddMovieDto>()))
            .Callback<AddMovieDto>(m =>
                {
                    var movie = new Movie
                    {
                        Title = m.Title,
                        Genre = m.Genre,
                        Year = m.Year
                    };

                    _movies.Add(movie);
                }
            )
            .Returns<AddMovieDto>(m => 
                new MovieDto
                {
                    Title = m.Title,
                    Genre = m.Genre,
                    Year = m.Year 
                });
        
        // act
        var result = (ObjectResult)_controller.AddMovie(addMovieDto).Result!;
        var resultValue = (MovieDto)result.Value!;

        // assert
        Assert.NotNull(resultValue);
        Assert.NotEmpty(_movies);
        Assert.Equal(201, result.StatusCode);
        Assert.Equal(addMovieDto.Title, _movies.First().Title);
        Assert.Equal(addMovieDto.Title,resultValue.Title);
        Assert.Equal(0.0m, resultValue.Rating);
    }

    [Fact]
    public void AddMovie_GivenExistingMovie_ThrowsBadRequestException()
    {
        // arrange
        var addMovieDto = new AddMovieDto
        {
            Title = "The Godfather",
            Genre = "Crime",
            Year = 1972
        };

        // act
        _service
            .Setup(s => s.AddMovie(It.IsAny<AddMovieDto>()))
            .Returns(() =>
            {
                if (_movies.Exists(m => m.Title == addMovieDto.Title))
                    throw new BadRequestException("Movie already exists");
                
                return new MovieDto();
            });
        
        // arrange
        var exception = Assert.Throws<BadRequestException>(() => _controller.AddMovie(addMovieDto));
        Assert.Equal("Movie already exists", exception.Message);
    }

    [Fact]
    public void UpdateMovie_GivenExistingId_UpdatesMovieData()
    {
        // arrange
        var updateMovieDto = new UpdateMovieDto
        {
            Title = "The Matrix",
            Genre = "Sci-Fi",
            Year = 1999
        };
        
        // act
        var result = (ObjectResult)_controller.UpdateMovie(_id, updateMovieDto);
        
        // assert
        Assert.Equal(200, result.StatusCode);
        Assert.Equal("The Matrix", _movies.First(m => m.Id == _id).Title);
        Assert.Equal("Sci-Fi", _movies.First(m => m.Id == _id).Genre);
        Assert.Equal((ushort)1999, _movies.First(m => m.Id == _id).Year);
    }

    [Fact]
    public void UpdateMovie_GivenNonExistingId_ThrowsNotFoundException()
    {
        // arrange
        _id = -1;
        
        // act
        var act = () => _controller.UpdateMovie(_id, new UpdateMovieDto());
        
        // assert
        var exception = Assert.Throws<NotFoundException>(act);
        Assert.Equal("Movie not found", exception.Message);
    }

    [Fact]
    public void DeleteMovie_GivenExistingId_DeletesMovie()
    {
        // act
        var result = (NoContentResult)_controller.DeleteMovie(_id);
        
        // assert
        Assert.Equal(204, result.StatusCode);
        Assert.Single(_movies);
        Assert.False(_movies.Exists(m => m.Id == _id));
    }

    [Fact]
    public void DeleteMovie_GivenNonExistingId_ThrowsNotFoundException()
    {
        // arrange
        _id = -1;
        
        // act
        var act = () => _controller.DeleteMovie(_id);
        
        // arrange
        var exception = Assert.Throws<NotFoundException>(act);
        Assert.Equal("Movie not found", exception.Message);
    }
}