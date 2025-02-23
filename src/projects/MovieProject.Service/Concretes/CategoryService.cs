using Core.CrossCuttingConcerns.Validation;
using FluentValidation;
using MovieProject.DataAccess.Repositories.Abstracts;
using MovieProject.Model.Dtos.Categories;
using MovieProject.Model.Entities;
using MovieProject.Service.Abstracts;
using MovieProject.Service.BusinessRules.Categories;
using MovieProject.Service.Mappers.Categories;
namespace MovieProject.Service.Concretes;

public sealed class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryMapper _categoryMapper;
    private readonly CategoryBusinessRules _businessRules;
    private readonly IValidator<CategoryAddRequestDto> _categoryAddValidator;

    public CategoryService(ICategoryRepository categoryRepository, ICategoryMapper categoryMapper, CategoryBusinessRules businessRules, IValidator<CategoryAddRequestDto> categoryAddValidator)
    {
        _categoryRepository = categoryRepository;
        _categoryMapper = categoryMapper;
        _businessRules = businessRules;
        _categoryAddValidator = categoryAddValidator;
    }

    public async Task AddAsync(CategoryAddRequestDto dto)
    {

        // Validasyon kuralları
        await ValidationTool.ValidateAsync(_categoryAddValidator,dto);
        // iş kuralı
        _businessRules.CategoryNameMustBeUnique(dto.Name);

        Category category = _categoryMapper.ConvertToEntity(dto);
       await _categoryRepository.AddAsync(category);
    }

    public async Task DeleteAsync(int id)
    {
        _businessRules.CategoryIsPresent(id);

        var category = await _categoryRepository.GetAsync(x=>x.Id==id);
        await _categoryRepository.DeleteAsync(category!);
    }

    public async Task<List<CategoryResponseDto>> GetAllAsync()
    {
        List<Category> categories = await _categoryRepository.GetAllAsync();
        List<CategoryResponseDto> responses = _categoryMapper.ConvertToResponseList(categories);
        return responses;
    }

    public async Task<CategoryResponseDto?> GetByIdAsync(int id)
    {
        _businessRules.CategoryIsPresent(id);
        Category? category = await _categoryRepository.GetAsync(x => x.Id == id);



        CategoryResponseDto categoryResponseDto = _categoryMapper.ConvertToResponse(category);
        return categoryResponseDto;
    }

    public async Task UpdateAsync(CategoryUpdateRequestDto dto)
    {
        _businessRules.CategoryIsPresent(dto.Id);

        Category? category = await _categoryRepository.GetAsync(x => x.Id == dto.Id);
        category.Name = dto.Name;

        _categoryRepository.Update(category);
    }
}
