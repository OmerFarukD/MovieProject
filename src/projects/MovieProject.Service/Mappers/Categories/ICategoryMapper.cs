using MovieProject.Model.Dtos.Categories;
using MovieProject.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Service.Mappers.Categories
{
    public interface ICategoryMapper
    {
        Category ConvertToEntity(CategoryAddRequestDto dto);
        Category ConvertToEntity(CategoryUpdateRequestDto dto);
        CategoryResponseDto ConvertToResponse(Category category);
        List<CategoryResponseDto> ConvertToResponseList(List<Category> categories);
    }
}
