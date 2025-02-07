using MovieProject.DataAccess.Repositories.Abstracts;
using MovieProject.Model.Entities;

namespace MovieProject.DataAccess.Repositories.Concretes;

public sealed class DapperCategoryRepository : ICategoryRepository
{
    public Category Add(Category entity)
    {
        throw new NotImplementedException();
    }

    public Category Delete(Category entity)
    {
        throw new NotImplementedException();
    }

    public List<Category> GetAll(bool include = true)
    {
        throw new NotImplementedException();
    }

    public Category? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Category> Query()
    {
        throw new NotImplementedException();
    }

    public Category Update(Category entity)
    {
        throw new NotImplementedException();
    }
}
