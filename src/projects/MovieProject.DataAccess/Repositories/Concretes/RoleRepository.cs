using Core.DataAccess.Repositories;
using Core.Security.Entities;
using MovieProject.DataAccess.Contexts;
using MovieProject.DataAccess.Repositories.Abstracts;

namespace MovieProject.DataAccess.Repositories.Concretes;

public sealed class RoleRepository : EfRepositoryBase<Role, int, BaseDbContext>, IRoleRepository
{
    public RoleRepository(BaseDbContext context) : base(context)
    {
    }
}
