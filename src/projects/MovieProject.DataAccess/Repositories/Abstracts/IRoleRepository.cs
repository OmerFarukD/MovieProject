using Core.DataAccess.Repositories;
using Core.Security.Entities;

namespace MovieProject.DataAccess.Repositories.Abstracts;

public interface IRoleRepository : IAsyncRepository<Role, int>, IRepository<Role, int>
{

}
