using AutoMapper;
using MovieProject.Model.Dtos.Categories;
using MovieProject.Model.Entities;

namespace MovieProject.Service.Mappers.Categories;

public sealed class CategoryAutoMapper : ICategoryMapper
{
    private readonly IMapper _mapper;

    public CategoryAutoMapper(IMapper mapper)
    {
        _mapper = mapper;
    }

    public Category ConvertToEntity(CategoryAddRequestDto dto)
    {
        return _mapper.Map<Category>(dto);
    }

    public Category ConvertToEntity(CategoryUpdateRequestDto dto)
    {
        return _mapper.Map<Category>(dto);
    }

    public CategoryResponseDto ConvertToResponse(Category category)
    {
        return _mapper.Map<CategoryResponseDto>(category);
    }

    public List<CategoryResponseDto> ConvertToResponseList(List<Category> categories)
    {
        return _mapper.Map<List<CategoryResponseDto>>(categories);
    }
}
