using Core.DataAccess.Repositories;
using Core.Security.Entities;
using MovieProject.DataAccess.Contexts;
using MovieProject.DataAccess.Repositories.Abstracts;

namespace MovieProject.DataAccess.Repositories.Concretes;

public sealed class UserRoleRepository : EfRepositoryBase<UserRole, int, BaseDbContext>, IUserRoleRepository
{
    public UserRoleRepository(BaseDbContext context) : base(context)
    {
    }
}