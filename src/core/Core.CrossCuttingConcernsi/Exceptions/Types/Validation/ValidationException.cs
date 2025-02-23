namespace Core.CrossCuttingConcerns.Exceptions.Types.Validation;

public sealed class ValidationException : Exception
{
    public IEnumerable<ValidationExceptionModel> Errors { get; }


    public ValidationException(string message) : base(message)
    {
        Errors = new List<ValidationExceptionModel>();
    }



    public ValidationException(IEnumerable<ValidationExceptionModel> errors) : base(BuildErrorMessage(errors))
    {
        Errors = errors;
    }


    private static string BuildErrorMessage(IEnumerable<ValidationExceptionModel> errors)
    {
        IEnumerable<string> err = errors.Select(x => $"{x.Property} : {string.Join(string.Empty,x.Errors)}");
        return string.Join(string.Empty, err);
    }

}
