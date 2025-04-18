namespace MovieProject.Service.BusinessRules.Directors;

public interface IDirectorBusinessRules
{
    Task DirectorNameMustBeUnique(string directorName, string surname);
    Task DirectorIsPresent(long id);

}
