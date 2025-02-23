using MovieProject.Model.Dtos.Categories;

namespace MovieProject.Service.Abstracts;

public interface ICategoryService
{
    Task AddAsync(CategoryAddRequestDto dto);
    Task UpdateAsync(CategoryUpdateRequestDto dto);

    Task<List<CategoryResponseDto>> GetAllAsync();

    Task<CategoryResponseDto?> GetByIdAsync(int id);

    Task DeleteAsync(int id);
}
