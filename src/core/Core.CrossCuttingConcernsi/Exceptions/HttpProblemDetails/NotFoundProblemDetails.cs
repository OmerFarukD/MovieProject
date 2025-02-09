using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;

public sealed class NotFoundProblemDetails : ProblemDetails
{
    public NotFoundProblemDetails(string detail)
    {
        Title = "Not Found Exception";
        Detail = detail;
        Status = StatusCodes.Status404NotFound;
        Type = "http://example.com/problems/notfound";
    }
}
