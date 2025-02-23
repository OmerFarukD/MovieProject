using MovieProject.Model.Dtos.Directors;

namespace MovieProject.Service.Abstracts;

public  interface IDirectorService
{
    Task<string> AddAsync(DirectorAddRequestDto dto);
    Task<string> UpdateAsync(DirectorUpdateRequestDto dto);
    Task<string> DeleteAsync(long Id);

    Task<DirectorResponseDto> GetByIdAsync(long id);
    Task<List<DirectorResponseDto>> GetAllAsync();



}
