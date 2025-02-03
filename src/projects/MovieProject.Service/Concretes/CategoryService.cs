using MovieProject.DataAccess.Contexts;
using MovieProject.DataAccess.Repositories.Abstracts;
using MovieProject.DataAccess.Repositories.Concretes;
using MovieProject.Model.Dtos.Categories;
using MovieProject.Model.Entities;
using MovieProject.Service.Abstracts;
using MovieProject.Service.Mappers;

namespace MovieProject.Service.Concretes;

public sealed class CategoryService : ICategoryService
{

    private readonly ICategoryRepository _categoryRepository;
    private readonly CategoryMapper _categoryMapper;

    public CategoryService(ICategoryRepository categoryRepository, CategoryMapper categoryMapper)
    {
        _categoryRepository = categoryRepository;
        _categoryMapper = categoryMapper;
    }

    public void Add(CategoryAddRequestDto dto)
    {
        Category category = _categoryMapper.ConvertToEntity(dto);
        _categoryRepository.Add(category);
    }

    public void Delete(int id)
    {
        var category = _categoryRepository.GetById(id);
        if(category == null)
        {
            //todo:  exception fırlat
        }

        _categoryRepository.Delete(category!);
    }

    public List<CategoryResponseDto> GetAll()
    {
        List<Category> categories = _categoryRepository.GetAll();
        List<CategoryResponseDto> responses = _categoryMapper.ConvertToResponseList(categories);
        return responses;
    }

    public CategoryResponseDto? GetById(int id)
    {
        Category? category = _categoryRepository.GetById(id);
        if(category is null)
        {
            // todo : Exception Fırlat
        }


        CategoryResponseDto categoryResponseDto = _categoryMapper.ConvertToResponse(category);
        return categoryResponseDto;
    }

    public void Update(CategoryUpdateRequestDto dto)
    {
        Category? category = _categoryRepository.GetById(dto.Id);
        if (category is null)
        {
            // todo : Exception Fırlat
        }


        category.Name = dto.Name;

        _categoryRepository.Update(category);
    }
}
