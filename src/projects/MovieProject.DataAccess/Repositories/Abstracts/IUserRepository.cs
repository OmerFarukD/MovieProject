using Core.DataAccess.Repositories;
using Core.Security.Entities;

namespace MovieProject.DataAccess.Repositories.Abstracts;

public interface IUserRepository : IAsyncRepository<User,int>, IRepository<User,int>
{
}
