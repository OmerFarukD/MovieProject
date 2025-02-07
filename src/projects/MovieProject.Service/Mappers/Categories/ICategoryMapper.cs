using MovieProject.Model.Dtos.Categories;
using MovieProject.Model.Entities;
namespace MovieProject.Service.Mappers.Categories;

public interface ICategoryMapper
{
    Category ConvertToEntity(CategoryAddRequestDto dto);
    Category ConvertToEntity(CategoryUpdateRequestDto dto);
    CategoryResponseDto ConvertToResponse(Category category);
    List<CategoryResponseDto> ConvertToResponseList(List<Category> categories);
}
