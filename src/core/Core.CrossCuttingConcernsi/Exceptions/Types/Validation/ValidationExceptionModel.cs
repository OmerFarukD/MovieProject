namespace Core.CrossCuttingConcerns.Exceptions.Types.Validation;

public class ValidationExceptionModel
{

    public string? Property { get; set; }

    public List<string>? Errors { get; set; }
}
