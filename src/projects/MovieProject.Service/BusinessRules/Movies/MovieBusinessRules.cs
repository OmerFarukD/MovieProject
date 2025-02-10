
using Core.CrossCuttingConcerns.Exceptions.Types;
using MovieProject.DataAccess.Repositories.Abstracts;
using MovieProject.Service.Constants.Movies;

namespace MovieProject.Service.BusinessRules.Movies;

public sealed class MovieBusinessRules(IMovieRepository movieRepository)
{
    public async Task MovieNameMutBeUniqueAsync(string name)
    {
        var isPresent = await movieRepository.AnyAsync(x=>x.Name==name);
        if (isPresent)
        {
            throw new BusinessException(MovieMessages.MovieNameMustBeUniqueMessage);
        }
    }

    public async Task MovieIsPresentAsync(Guid id)
    {
        var isPresent = await movieRepository.AnyAsync(x => x.Id == id);
        if (!isPresent)
        {
            throw new NotFoundException(MovieMessages.MovieNotFoundMessage);
        }
    }

}
