using FluentValidation;

namespace Core.CrossCuttingConcerns.Validation;

public  static class ValidationTool
{

    public static async Task Validate(IValidator validator, object dto)
    {
        var context = new ValidationContext<object>(dto);
        var result = await validator.ValidateAsync(context);

        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
    }
}
