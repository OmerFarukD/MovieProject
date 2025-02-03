using Core.DataAccess.Repositories;
using MovieProject.DataAccess.Contexts;
using MovieProject.DataAccess.Repositories.Abstracts;
using MovieProject.Model.Entities;

namespace MovieProject.DataAccess.Repositories.Concretes;

public class DirectorRepository : EfRepositoryBase<Director, long, BaseDbContext>, IDirectorRepository
{
    public DirectorRepository(BaseDbContext context) : base(context)
    {
    }

    // Ahmet 
    // ahm
    public List<Director> GetAllByNameContains(string name)
    {
        return Context.Directors.Where(x => x.Name.Contains(name,StringComparison.OrdinalIgnoreCase)).ToList();
    }
}


