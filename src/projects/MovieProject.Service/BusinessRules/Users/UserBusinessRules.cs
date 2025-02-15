using Core.CrossCuttingConcerns.Exceptions.Types;
using MovieProject.DataAccess.Repositories.Abstracts;
using MovieProject.Service.Constants.Users;

namespace MovieProject.Service.BusinessRules.Users;

public class UserBusinessRules(IUserRepository userRepository)
{
    public async Task UsernameMustBeUniqueAsync(string username)
    {
        bool isPresent = await userRepository.AnyAsync(x=>x.Username==username);
        if (isPresent)
            throw new NotFoundException(UserMessages.UsernameMustBeUnique);
    }

    public async Task EmailMustBeUniqueAsync(string email)
    {
        bool isPresent = await userRepository.AnyAsync(x => x.Email == email);
        if(isPresent)
            throw new NotFoundException(UserMessages.EmailMustBeUnique);
    }


    public async Task UserIsPresent(int id)
    {
        bool isPresent = await userRepository.AnyAsync(x=>x.Id == id);
        if (!isPresent)
            throw new NotFoundException(UserMessages.UserNotFound);
    }


}
