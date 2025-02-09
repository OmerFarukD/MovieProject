using Core.CrossCuttingConcerns.Exceptions.Types;

namespace Core.CrossCuttingConcerns.Exceptions.Handlers;

public abstract class ExceptionHandler
{

    public Task HandleExceptionAsync(Exception exception)
    {
        return exception switch
        {
            BusinessException businessException => HandleException(businessException),
            NotFoundException notFoundException => HandleException(notFoundException),
            _ => HandleException(exception)
        };
    }

    // switch expression

    protected abstract Task HandleException(NotFoundException notFoundException);
    protected abstract Task HandleException(BusinessException businessException);

    protected abstract Task HandleException(Exception exception);
}


