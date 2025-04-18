using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Moq;
using MovieProject.DataAccess.Repositories.Abstracts;
using MovieProject.Model.Dtos.Directors;
using MovieProject.Model.Entities;
using MovieProject.Service.BusinessRules.Directors;
using MovieProject.Service.Concretes;
using MovieProject.Service.Constants.Directors;
using MovieProject.Service.Helpers;
using Service.Test.Utils;

namespace Service.Test.Services;

public class DirectorServiceTests
{
    private DirectorService _directorService;
    private Mock<IMapper> _mapperMock;
    private Mock<IDirectorRepository> _directorRepositoryMock;
    private Mock<ICloudinaryHelper> _cloudinaryHelperMock;
    private Mock<IDirectorBusinessRules> _businessRulesMock;

    [SetUp]
    public void SetUp()
    {
        _mapperMock = new Mock<IMapper>();
        _directorRepositoryMock = new Mock<IDirectorRepository>();
        _cloudinaryHelperMock = new Mock<ICloudinaryHelper>();
        _businessRulesMock = new Mock<IDirectorBusinessRules>();

        _directorService = new DirectorService(_mapperMock.Object,_directorRepositoryMock.Object,
            _cloudinaryHelperMock.Object,_businessRulesMock.Object
            );
    }



    [Test]
    public async Task AddAsync_WhenDirectorIsNotPresent_ShouldReturnSuccess()
    {

        // Arrange
        var dto = DirectorUtils.AddRequestDto();
        var director = DirectorUtils.AddMapDirector();
        string url = "deneme url";

        _mapperMock.Setup(s => s.Map<Director>(dto)).Returns(director);

        _cloudinaryHelperMock.Setup(s => s.UploadImageAsync(dto.Image, "test")).ReturnsAsync(url);


        // Act
        var result = await _directorService.AddAsync(dto);



        // Assert

        Assert.AreEqual(result, DirectorMessages.DirectorAdded);
    }


    [Test]
    public async Task AddAsync_WhenDirectorIsPresent_ShouldThrowBusinessException()
    {
        var dto = DirectorUtils.AddRequestDto();
        _businessRulesMock.Setup(s => s.DirectorNameMustBeUnique(dto.Name, dto.Surname))
            .ThrowsAsync(new BusinessException("Yönetmen adı benzersiz olmalıdır."));

        var result = Assert.ThrowsAsync<BusinessException>(() =>

        _directorService.AddAsync(dto)
        );
        Assert.AreEqual(result.Message, DirectorMessages.DirectorMustBeUnique);

    }


    [Test]
    public async Task GetAllAsync_WhenDirectorsIsPresent_ShouldReturnResponseDtoList()
    {
        // Arrange 

        var directors = DirectorUtils.DirectorsList();
        var responses = DirectorUtils.ResponseList();

        _directorRepositoryMock.Setup(s => s.GetAllAsync(null, true, false, default)).ReturnsAsync(directors);
        _mapperMock.Setup(s => s.Map<List<DirectorResponseDto>>(directors)).Returns(responses);



        // Act 
        var result = await _directorService.GetAllAsync();


        // Assert

        Assert.AreEqual(result,responses);


    }

}
