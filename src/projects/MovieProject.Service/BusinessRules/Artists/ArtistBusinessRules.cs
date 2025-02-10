using Core.CrossCuttingConcerns.Exceptions.Types;
using MovieProject.DataAccess.Repositories.Abstracts;
using MovieProject.Service.Constants.Artists;

namespace MovieProject.Service.BusinessRules.Artists;

public sealed class ArtistBusinessRules(IArtistRepository artistRepository)
{

    public async Task ArtistFullNameMustBeUniqueAsync(string firstName,string lastName)
    {
        var isPresent = await artistRepository
            .AnyAsync(x=>x.Name==firstName && x.Surname==lastName);

        if (isPresent)
        {
            throw new BusinessException(ArtistMessages.ArtistFullNameMustBeUniqueMessage);
        }
    }

    public async Task ArtistIsPresentAsync(long id)
    {
        var isPresent = await artistRepository
          .AnyAsync(x => x.Id ==id);

        if (!isPresent)
        {
            throw new NotFoundException(ArtistMessages.ArtistNotFoundMessage);
        }
    }

}
