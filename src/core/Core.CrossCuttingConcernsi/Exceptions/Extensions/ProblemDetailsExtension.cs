using System.Text.Json;

namespace Core.CrossCuttingConcerns.Exceptions.Extensions;

public static class ProblemDetailsExtension
{
    public static string AsJson<TProblemDetails>( this TProblemDetails details)
    {
        return JsonSerializer.Serialize(details);
    }
}
