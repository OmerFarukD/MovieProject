using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.CrossCuttingConcerns.Exceptions.Types.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.Handlers;

public sealed class HttpExceptionHandler : ExceptionHandler
{

    public HttpResponse Response { get; set; }

    protected override Task HandleException(NotFoundException notFoundException)
    {
        Response.StatusCode = StatusCodes.Status404NotFound;
        NotFoundProblemDetails problem = new NotFoundProblemDetails(notFoundException.Message);
        string details = JsonSerializer.Serialize(problem);

        return Response.WriteAsync(details);
    }

    protected override Task HandleException(BusinessException businessException)
    {
        Response.StatusCode = StatusCodes.Status400BadRequest;
        string details = new BusinessProblemDetails(businessException.Message).AsJson();
        return Response.WriteAsync(details);
    }

    protected override Task HandleException(Exception exception)
    {
        Response.StatusCode = StatusCodes.Status500InternalServerError;
        string details = new ServerErrorProblemDetails().AsJson();
        return Response.WriteAsync(details);
    }

    protected override Task HandleException(ValidationException exception)
    {
        Response.StatusCode = StatusCodes.Status400BadRequest;
        string details = new ValidationProblemDetails(exception.Errors).AsJson();
        return Response.WriteAsync(details);
    }
}
