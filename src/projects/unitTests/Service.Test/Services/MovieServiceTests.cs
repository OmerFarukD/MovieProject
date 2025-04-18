namespace Service.Test.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Core.CrossCuttingConcerns.Cache.Redis;
    using global::MovieProject.DataAccess.Repositories.Abstracts;
    using global::MovieProject.Model.Dtos.Movies;
    using global::MovieProject.Model.Entities;
    using global::MovieProject.Service.BusinessRules.Movies;
    using global::MovieProject.Service.Concretes;
    using global::MovieProject.Service.Constants.Movies;
    using global::MovieProject.Service.Helpers;
    using Moq;
    using NUnit.Framework;

    namespace MovieProject.Service.Tests
    {
        [TestFixture]
        public class MovieServiceTests
        {
            private Mock<IMovieRepository> _movieRepositoryMock;
            private Mock<IMapper> _mapperMock;
            private Mock<ICloudinaryHelper> _cloudinaryHelperMock;
            private Mock<MovieBusinessRules> _businessRulesMock;
            private Mock<IRedisCacheService> _cacheMock;
            private MovieService _movieService;

            [SetUp]
            public void SetUp()
            {
                _movieRepositoryMock = new Mock<IMovieRepository>();
                _mapperMock = new Mock<IMapper>();
                _cloudinaryHelperMock = new Mock<ICloudinaryHelper>();
                _businessRulesMock = new Mock<MovieBusinessRules>();
                _cacheMock = new Mock<IRedisCacheService>();

                _movieService = new MovieService(
                    _movieRepositoryMock.Object,
                    _mapperMock.Object,
                    _cloudinaryHelperMock.Object,
                    _businessRulesMock.Object,
                    _cacheMock.Object);
            }

            [Test]
            public async Task AddAsync_ShouldAddMovieSuccessfully()
            {
                // Arrange
                var dto = new MovieAddRequestDto { Name = "Test Movie", Image = "imageData" };
                var movie = new Movie();
                var imageUrl = "http://image.url";

                _cacheMock.Setup(x => x.RemoveDataAsync(RedisMovieKey.MovieListKey))
                          .Returns(Task.CompletedTask);
                _businessRulesMock.Setup(x => x.MovieNameMutBeUniqueAsync(dto.Name))
                                  .Returns(Task.CompletedTask);
                _mapperMock.Setup(x => x.Map<Movie>(dto))
                           .Returns(movie);
                _cloudinaryHelperMock.Setup(x => x.UploadImageAsync(dto.Image, "movie-store"))
                                     .ReturnsAsync(imageUrl);
                _movieRepositoryMock.Setup(x => x.AddAsync(movie, It.IsAny<CancellationToken>()))
                                    .Returns((Task<Movie>)Task.CompletedTask);

                // Act
                var result = await _movieService.AddAsync(dto);

                // Assert
                Assert.AreEqual(MovieMessages.MovieAddedMessage, result);
                Assert.AreEqual(imageUrl, movie.ImageUrl);
                _cacheMock.Verify(x => x.RemoveDataAsync(RedisMovieKey.MovieListKey), Times.Once);
                _businessRulesMock.Verify(x => x.MovieNameMutBeUniqueAsync(dto.Name), Times.Once);
                _cloudinaryHelperMock.Verify(x => x.UploadImageAsync(dto.Image, "movie-store"), Times.Once);
                _movieRepositoryMock.Verify(x => x.AddAsync(movie, It.IsAny<CancellationToken>()), Times.Once);
            }

            [Test]
            public async Task DeleteAsync_ShouldDeleteMovieSuccessfully()
            {
                // Arrange
                var movieId = Guid.NewGuid();
                var movie = new Movie { Id = movieId };

                _cacheMock.Setup(x => x.RemoveDataAsync(RedisMovieKey.MovieListKey))
                          .Returns(Task.CompletedTask);
                _cacheMock.Setup(x => x.RemoveDataAsync(RedisMovieKey.GetByIdKey(movieId)))
                          .Returns(Task.CompletedTask);
                _businessRulesMock.Setup(x => x.MovieIsPresentAsync(movieId))
                                  .Returns(Task.CompletedTask);
                _movieRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Movie, bool>>>(), false, ))
                                    .ReturnsAsync(movie);
                _movieRepositoryMock.Setup(x => x.DeleteAsync(movie, It.IsAny<CancellationToken>()))
                                    .Returns((Task<Movie>)Task.CompletedTask);

                // Act
                await _movieService.DeleteAsync(movieId);

                // Assert
                _cacheMock.Verify(x => x.RemoveDataAsync(RedisMovieKey.MovieListKey), Times.Once);
                _cacheMock.Verify(x => x.RemoveDataAsync(RedisMovieKey.GetByIdKey(movieId)), Times.Once);
                _businessRulesMock.Verify(x => x.MovieIsPresentAsync(movieId), Times.Once);
                _movieRepositoryMock.Verify(x => x.GetAsync(It.IsAny<Func<Movie, bool>>(), false, It.IsAny<CancellationToken>()), Times.Once);
                _movieRepositoryMock.Verify(x => x.DeleteAsync(movie, It.IsAny<CancellationToken>()), Times.Once);
            }

            [Test]
            public async Task GetAllAsync_ShouldReturnCachedMovies_IfCacheExists()
            {
                // Arrange
                var cachedMovies = new List<MovieResponseDto> { new MovieResponseDto() };
                _cacheMock.Setup(x => x.GetDataAsync<List<MovieResponseDto>>(RedisMovieKey.MovieListKey))
                          .ReturnsAsync(cachedMovies);

                // Act
                var result = await _movieService.GetAllAsync();

                // Assert
                Assert.AreEqual(cachedMovies, result);
                _cacheMock.Verify(x => x.GetDataAsync<List<MovieResponseDto>>(RedisMovieKey.MovieListKey), Times.Once);
                _movieRepositoryMock.Verify(x => x.GetAllAsync(false, It.IsAny<CancellationToken>()), Times.Never);
            }

            [Test]
            public async Task GetAllAsync_ShouldReturnMoviesFromRepository_IfCacheIsEmpty()
            {
                // Arrange
                _cacheMock.Setup(x => x.GetDataAsync<List<MovieResponseDto>>(RedisMovieKey.MovieListKey))
                          .ReturnsAsync((List<MovieResponseDto>)null);
                var movies = new List<Movie> { new Movie() };
                var movieResponseDtos = new List<MovieResponseDto> { new MovieResponseDto() };

                _movieRepositoryMock.Setup(x => x.GetAllAsync(false, It.IsAny<CancellationToken>()))
                                    .ReturnsAsync(movies);
                _mapperMock.Setup(x => x.Map<List<MovieResponseDto>>(movies))
                           .Returns(movieResponseDtos);
                _cacheMock.Setup(x => x.SetDataAsync(RedisMovieKey.MovieListKey, movieResponseDtos))
                          .Returns(Task.CompletedTask);

                // Act
                var result = await _movieService.GetAllAsync();

                // Assert
                Assert.AreEqual(movieResponseDtos, result);
                _movieRepositoryMock.Verify(x => x.GetAllAsync(false, It.IsAny<CancellationToken>()), Times.Once);
                _cacheMock.Verify(x => x.SetDataAsync(RedisMovieKey.MovieListKey, movieResponseDtos), Times.Once);
            }

            [Test]
            public async Task GetAllByCategoryIdAsync_ShouldReturnMoviesByCategory()
            {
                // Arrange
                int categoryId = 1;
                var movies = new List<Movie> { new Movie { CategoryId = categoryId } };
                var movieResponseDtos = new List<MovieResponseDto> { new MovieResponseDto() };

                _movieRepositoryMock.Setup(x => x.GetAllAsync(
                        It.IsAny<Func<Movie, bool>>(), false, It.IsAny<CancellationToken>()))
                                    .ReturnsAsync(movies);
                _mapperMock.Setup(x => x.Map<List<MovieResponseDto>>(movies))
                           .Returns(movieResponseDtos);

                // Act
                var result = await _movieService.GetAllByCategoryIdAsync(categoryId);

                // Assert
                Assert.AreEqual(movieResponseDtos, result);
                _movieRepositoryMock.Verify(x => x.GetAllAsync(
                    It.Is<Func<Movie, bool>>(f => f(new Movie { CategoryId = categoryId })),
                    false, It.IsAny<CancellationToken>()), Times.Once);
            }

            [Test]
            public async Task GetAllByDirectorIdAsync_ShouldReturnMoviesByDirector()
            {
                // Arrange
                long directorId = 123;
                var movies = new List<Movie> { new Movie { DirectorId = directorId } };
                var movieResponseDtos = new List<MovieResponseDto> { new MovieResponseDto() };

                _movieRepositoryMock.Setup(x => x.GetAllAsync(
                        It.IsAny<Func<Movie, bool>>(), false, It.IsAny<CancellationToken>()))
                                    .ReturnsAsync(movies);
                _mapperMock.Setup(x => x.Map<List<MovieResponseDto>>(movies))
                           .Returns(movieResponseDtos);

                // Act
                var result = await _movieService.GetAllByDirectorIdAsync(directorId);

                // Assert
                Assert.AreEqual(movieResponseDtos, result);
                _movieRepositoryMock.Verify(x => x.GetAllAsync(
                    It.Is<Func<Movie, bool>>(f => f(new Movie { DirectorId = directorId })),
                    false, It.IsAny<CancellationToken>()), Times.Once);
            }

            [Test]
            public async Task GetAllByImdbRangeAsync_ShouldReturnMoviesWithinRange()
            {
                // Arrange
                double min = 5.0, max = 9.0;
                var movies = new List<Movie> { new Movie { IMDB = 7.0 } };
                var movieResponseDtos = new List<MovieResponseDto> { new MovieResponseDto() };

                _movieRepositoryMock.Setup(x => x.GetAllAsync(
                        It.IsAny<Func<Movie, bool>>(), false, It.IsAny<CancellationToken>()))
                                    .ReturnsAsync(movies);
                _mapperMock.Setup(x => x.Map<List<MovieResponseDto>>(movies))
                           .Returns(movieResponseDtos);

                // Act
                var result = await _movieService.GetAllByImdbRangeAsync(min, max);

                // Assert
                Assert.AreEqual(movieResponseDtos, result);
                _movieRepositoryMock.Verify(x => x.GetAllAsync(
                    It.Is<Func<Movie, bool>>(f => f(new Movie { IMDB = 7.0 })),
                    false, It.IsAny<CancellationToken>()), Times.Once);
            }

            [Test]
            public async Task GetAllByIsActiveAsync_ShouldReturnMoviesByActiveStatus()
            {
                // Arrange
                bool active = true;
                var movies = new List<Movie> { new Movie { IsActive = active } };
                var movieResponseDtos = new List<MovieResponseDto> { new MovieResponseDto() };

                _movieRepositoryMock.Setup(x => x.GetAllAsync(
                        It.IsAny<Func<Movie, bool>>(), false, It.IsAny<CancellationToken>()))
                                    .ReturnsAsync(movies);
                _mapperMock.Setup(x => x.Map<List<MovieResponseDto>>(movies))
                           .Returns(movieResponseDtos);

                // Act
                var result = await _movieService.GetAllByIsActiveAsync(active);

                // Assert
                Assert.AreEqual(movieResponseDtos, result);
                _movieRepositoryMock.Verify(x => x.GetAllAsync(
                    It.Is<Func<Movie, bool>>(f => f(new Movie { IsActive = active })),
                    false, It.IsAny<CancellationToken>()), Times.Once);
            }

            [Test]
            public async Task GetByIdAsync_ShouldReturnCachedMovie_IfCacheExists()
            {
                // Arrange
                Guid movieId = Guid.NewGuid();
                string cacheKey = RedisMovieKey.GetByIdKey(movieId);
                var cachedMovie = new MovieResponseDto();

                _cacheMock.Setup(x => x.GetDataAsync<MovieResponseDto>(cacheKey))
                          .ReturnsAsync(cachedMovie);

                // Act
                var result = await _movieService.GetByIdAsync(movieId);

                // Assert
                Assert.AreEqual(cachedMovie, result);
                _cacheMock.Verify(x => x.GetDataAsync<MovieResponseDto>(cacheKey), Times.Once);
                _movieRepositoryMock.Verify(x => x.GetAsync(
                    It.IsAny<Func<Movie, bool>>(), false, It.IsAny<CancellationToken>()), Times.Never);
            }

            [Test]
            public async Task GetByIdAsync_ShouldReturnMovieFromRepository_IfCacheIsEmpty()
            {
                // Arrange
                Guid movieId = Guid.NewGuid();
                string cacheKey = RedisMovieKey.GetByIdKey(movieId);
                _cacheMock.Setup(x => x.GetDataAsync<MovieResponseDto>(cacheKey))
                          .ReturnsAsync((MovieResponseDto)null);
                _businessRulesMock.Setup(x => x.MovieIsPresentAsync(movieId))
                                  .Returns(Task.CompletedTask);
                var movie = new Movie { Id = movieId };
                var movieResponseDto = new MovieResponseDto();

                _movieRepositoryMock.Setup(x => x.GetAsync(
                        It.IsAny<Func<Movie, bool>>(), false, It.IsAny<CancellationToken>()))
                                    .ReturnsAsync(movie);
                _mapperMock.Setup(x => x.Map<MovieResponseDto>(movie))
                           .Returns(movieResponseDto);
                _cacheMock.Setup(x => x.SetDataAsync<MovieResponseDto>(cacheKey, movieResponseDto))
                          .Returns(Task.CompletedTask);

                // Act
                var result = await _movieService.GetByIdAsync(movieId);

                // Assert
                Assert.AreEqual(movieResponseDto, result);
                _cacheMock.Verify(x => x.GetDataAsync<MovieResponseDto>(cacheKey), Times.Once);
                _businessRulesMock.Verify(x => x.MovieIsPresentAsync(movieId), Times.Once);
                _movieRepositoryMock.Verify(x => x.GetAsync(
                    It.IsAny<Func<Movie, bool>>(), false, It.IsAny<CancellationToken>()), Times.Once);
                _mapperMock.Verify(x => x.Map<MovieResponseDto>(movie), Times.Once);
                _cacheMock.Verify(x => x.SetDataAsync<MovieResponseDto>(cacheKey, movieResponseDto), Times.Once);
            }

            [Test]
            public async Task UpdateAsync_ShouldUpdateMovieSuccessfully()
            {
                // Arrange
                var dto = new MovieUpdateRequestDto { Id = Guid.NewGuid() };
                _cacheMock.Setup(x => x.RemoveDataAsync(RedisMovieKey.MovieListKey))
                          .Returns(Task.CompletedTask);
                _cacheMock.Setup(x => x.RemoveDataAsync(RedisMovieKey.GetByIdKey(dto.Id)))
                          .Returns(Task.CompletedTask);
                _businessRulesMock.Setup(x => x.MovieIsPresentAsync(dto.Id))
                                  .Returns(Task.CompletedTask);

                var movie = new Movie { Id = dto.Id };
                _mapperMock.Setup(x => x.Map<Movie>(dto))
                           .Returns(movie);
                _movieRepositoryMock.Setup(x => x.UpdateAsync(movie, It.IsAny<CancellationToken>()))
                                    .Returns(Task.CompletedTask);

                // Act
                await _movieService.UpdateAsync(dto);

                // Assert
                _cacheMock.Verify(x => x.RemoveDataAsync(RedisMovieKey.MovieListKey), Times.Once);
                _cacheMock.Verify(x => x.RemoveDataAsync(RedisMovieKey.GetByIdKey(dto.Id)), Times.Once);
                _businessRulesMock.Verify(x => x.MovieIsPresentAsync(dto.Id), Times.Once);
                _mapperMock.Verify(x => x.Map<Movie>(dto), Times.Once);
                _movieRepositoryMock.Verify(x => x.UpdateAsync(movie, It.IsAny<CancellationToken>()), Times.Once);
            }
        }
    }

}
