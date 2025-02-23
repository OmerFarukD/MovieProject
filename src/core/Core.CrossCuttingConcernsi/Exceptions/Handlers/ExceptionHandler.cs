using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.CrossCuttingConcerns.Exceptions.Types.Validation;

namespace Core.CrossCuttingConcerns.Exceptions.Handlers;

public abstract class ExceptionHandler
{

    public Task HandleExceptionAsync(Exception exception)
    {
        return exception switch
        {
            BusinessException businessException => HandleException(businessException),
            NotFoundException notFoundException => HandleException(notFoundException),
            ValidationException validationException => HandleException(validationException),
            _ => HandleException(exception)
        };
    }

    // switch expression

    protected abstract Task HandleException(NotFoundException notFoundException);
    protected abstract Task HandleException(BusinessException businessException);

    protected abstract Task HandleException(Exception exception);


    protected abstract Task HandleException(ValidationException exception);
}


