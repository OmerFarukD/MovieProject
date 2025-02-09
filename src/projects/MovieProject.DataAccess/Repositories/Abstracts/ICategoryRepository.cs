using Core.DataAccess.Repositories;
using MovieProject.Model.Entities;

namespace MovieProject.DataAccess.Repositories.Abstracts;

public interface ICategoryRepository :IRepository<Category,int>, IAsyncRepository<Category,int>
{

}
