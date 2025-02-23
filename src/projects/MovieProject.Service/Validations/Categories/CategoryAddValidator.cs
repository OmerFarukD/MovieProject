using FluentValidation;
using MovieProject.Model.Dtos.Categories;
using MovieProject.Service.Constants.Categories;

namespace MovieProject.Service.Validations.Categories;

public class CategoryAddValidator : AbstractValidator<CategoryAddRequestDto>
{

    public CategoryAddValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage(CategoryMessages.NameNotBeEmpty)
            .MinimumLength(2).WithMessage(CategoryMessages.NameMinimumLengthMessage);
    }
}
