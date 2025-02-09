using Core.DataAccess.Repositories;
using MovieProject.Model.Entities;

namespace MovieProject.DataAccess.Repositories.Abstracts;

public interface IDirectorRepository: IRepository<Director,long>, IAsyncRepository<Director,long>
{
    List<Director> GetAllByNameContains(string name);
}
