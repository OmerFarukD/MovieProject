using AutoMapper;
using Microsoft.Identity.Client;
using MovieProject.DataAccess.Repositories.Abstracts;
using MovieProject.Model.Dtos.Directors;
using MovieProject.Model.Entities;
using MovieProject.Service.Abstracts;
using MovieProject.Service.BusinessRules.Directors;
using MovieProject.Service.Constants.Directors;
using MovieProject.Service.Helpers;

namespace MovieProject.Service.Concretes;

public sealed class DirectorService : IDirectorService
{

    private readonly IMapper _mapper;
    private readonly IDirectorRepository _directorRepository;
    private readonly ICloudinaryHelper _cloudinaryHelper;
    private readonly DirectorBusinessRules _directorBusinessRules;

    public DirectorService(IMapper mapper, IDirectorRepository directorRepository, ICloudinaryHelper cloudinaryHelper, DirectorBusinessRules directorBusinessRules)
    {
        _mapper = mapper;
        _directorRepository = directorRepository;
        _cloudinaryHelper = cloudinaryHelper;
        _directorBusinessRules = directorBusinessRules;
    }

    public async Task<string> AddAsync(DirectorAddRequestDto dto)
    {

        await _directorBusinessRules.DirectorNameMustBeUnique(dto.Name,dto.Surname);
        Director director = _mapper.Map<Director>(dto);

        string url = await _cloudinaryHelper.UploadImageAsync(dto.Image,"movies-director");
        director.ImageUrl = url;

        await _directorRepository.AddAsync(director);


        return DirectorMessages.DirectorAdded;


    }

    public async Task<string> DeleteAsync(long id)
    {
        await _directorBusinessRules.DirectorIsPresent(id);

        var director = await _directorRepository.GetAsync(x=>x.Id == id,include:false);

        await _directorRepository.DeleteAsync(director);

        return DirectorMessages.DirectorDeleted;
    }

    public async Task<List<DirectorResponseDto>> GetAllAsync()
    {
        List<Director> directors = await _directorRepository.GetAllAsync(enableTracking:false);

        List<DirectorResponseDto> responses = _mapper.Map<List<DirectorResponseDto>>(directors);

        return responses;

    }

    public async Task<DirectorResponseDto> GetByIdAsync(long id)
    {
        await _directorBusinessRules.DirectorIsPresent(id);

        Director director = await _directorRepository.GetAsync(x=>x.Id == id);

        DirectorResponseDto directorResponseDto = _mapper.Map<DirectorResponseDto>(director);

        return directorResponseDto;
    }

    public async Task<string> UpdateAsync(DirectorUpdateRequestDto dto)
    {
        await _directorBusinessRules.DirectorIsPresent(dto.Id);

        Director director = _mapper.Map<Director>(dto);

        await _directorRepository.UpdateAsync(director);

        return DirectorMessages.DirectorUpdated;
    }
}
