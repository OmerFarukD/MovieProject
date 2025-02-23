using Core.CrossCuttingConcerns.Exceptions.Types.Validation;
using FluentValidation;

namespace Core.CrossCuttingConcerns.Validation;

public  static class ValidationTool
{

    public static async Task ValidateAsync(IValidator validator, object dto)
    {
        var context = new ValidationContext<object>(dto);
        var result = await validator.ValidateAsync(context);

        if (!result.IsValid)
        {
            var errors = result.Errors.GroupBy(e => e.PropertyName)
                 .Select(x => new ValidationExceptionModel
                 {
                     Property = x.Key,
                     Errors = x.Select(e => e.ErrorMessage)
                 });

            throw new Exceptions.Types.Validation.ValidationException(errors);
        }
    }
}
