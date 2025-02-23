using Core.CrossCuttingConcerns.Exceptions.Types;
using MovieProject.DataAccess.Repositories.Abstracts;
using MovieProject.Service.Constants.Directors;

namespace MovieProject.Service.BusinessRules.Directors;
// ctrl + r +g
public sealed class DirectorBusinessRules
{
    private readonly IDirectorRepository _directorRepository;

    public DirectorBusinessRules(IDirectorRepository directorRepository)
    {
        _directorRepository = directorRepository;
    }


    public async Task DirectorNameMustBeUnique(string directorName , string surname)
    {
        var isPresent = await _directorRepository.AnyAsync(x=>x.Name==directorName && x.Surname ==surname);
        if (isPresent)
            throw new BusinessException(DirectorMessages.DirectorMustBeUnique);
    }


    public async Task DirectorIsPresent(long id)
    {
        var isPresent = await _directorRepository.AnyAsync(x=>x.Id == id);
        if (!isPresent)
            throw new NotFoundException(DirectorMessages.DirectorNotFound);
    }


}
