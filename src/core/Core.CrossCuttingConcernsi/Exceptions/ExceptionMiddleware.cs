
using Core.CrossCuttingConcerns.Exceptions.Handlers;
using Microsoft.AspNetCore.Http;

namespace Core.CrossCuttingConcerns.Exceptions;

public class ExceptionMiddleware
{

    private readonly HttpExceptionHandler _exceptionHandler;
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
        _exceptionHandler = new HttpExceptionHandler();
    }


    // async - await- Task 
    public async Task Invoke(HttpContext context)
    {

        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context.Response,e);
        }
    }

    private Task HandleExceptionAsync(HttpResponse response, Exception exception)
    {
        response.ContentType = "application/json";
        _exceptionHandler.Response = response;
        return _exceptionHandler.HandleExceptionAsync(exception);
    }
}
