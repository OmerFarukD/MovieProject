using Core.CrossCuttingConcerns.Exceptions.Types;
using Moq;
using MovieProject.DataAccess.Repositories.Abstracts;
using MovieProject.DataAccess.Repositories.Concretes;
using MovieProject.Model.Entities;
using MovieProject.Service.BusinessRules.Movies;
using MovieProject.Service.Constants.Movies;
using System.Linq.Expressions;

namespace Service.Test.BusinessRules;

public class MovieBusinessRulesTest
{
    // unit test fonksiyonları senkronsa void asenkron sa Task ile yazılmalıdır.
    private MovieBusinessRules _movieBusinessRules;
    private Mock<IMovieRepository> _movieRepository;


    [SetUp]
    public void SetUp()
    {
        _movieRepository = new Mock<IMovieRepository>();
        _movieBusinessRules = new MovieBusinessRules(_movieRepository.Object);
    }


    // MetodunAdı_Olay_BeklenenDavranış
    [Test]
    public async Task MovieNameMutBeUniqueAsync_WhenNameExists_ShouldThrowBusinessException()
    {
        // Arange : Verilerin hazırlandığı yer.
        string movieName = "Deneme";
        _movieRepository.Setup(repo=> repo.AnyAsync(It.IsAny<Expression<Func<Movie,bool>>>(),true,default)).ReturnsAsync(true);



        // Act & Assert

        var ex = Assert.ThrowsAsync<BusinessException>(() =>

        _movieBusinessRules.MovieNameMutBeUniqueAsync(movieName)
        );

        
        Assert.AreEqual(ex.Message, MovieMessages.MovieNameMustBeUniqueMessage);
    }


    [Test]
    public async Task MovieNameMutBeUniqueAsync_WhenDoesNotExists_ShouldNotThrowException()
    {
        // Arange
        string movieName = "X men";
        _movieRepository.Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Movie, bool>>>(), true, default)).ReturnsAsync(false);


        // Act & Assert
        Assert.DoesNotThrowAsync(() => _movieBusinessRules.MovieNameMutBeUniqueAsync(movieName));
    }




}
