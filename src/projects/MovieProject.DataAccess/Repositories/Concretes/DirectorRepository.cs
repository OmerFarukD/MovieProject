using Core.DataAccess.Repositories;
using MovieProject.DataAccess.Contexts;
using MovieProject.DataAccess.Repositories.Abstracts;
using MovieProject.Model.Entities;

namespace MovieProject.DataAccess.Repositories.Concretes;

public class DirectorRepository : IDirectorRepository
{
    private readonly BaseDbContext _dbContext;

    public DirectorRepository(BaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Director Add(Director entity)
    {

        entity.CreatedTime = DateTime.UtcNow;
        _dbContext.Directors.Add(entity);
        _dbContext.SaveChanges();

        return entity;

    }

    public Director Delete(Director entity)
    {
        _dbContext.Directors.Remove(entity);
        _dbContext.SaveChanges();

        return entity;
    }

    public List<Director> GetAll()
    {
        return _dbContext.Directors.ToList();
    }

    public List<Director> GetAllByNameContains(string name)
    {
        // Select * from Directors where name = '%name%'
        return _dbContext.Directors.Where(x => x.Name.Contains(name)).ToList();
        
    }

    public Director? GetById(long id)
    {
        //1. Yöntem
       // return _dbContext.Directors.Find(id);

        //2. Yöntem
        // return _dbContext.Directors.Where(x => x.Id == id).SingleOrDefault();

        //3. Yöntem 
        //return _dbContext.Directors.Where(x => x.Id == id).FirstOrDefault();

        //4. Yöntem
         return _dbContext.Directors.SingleOrDefault(x => x.Id == id);

        //5. Yöntem
        //return _dbContext.Directors.FirstOrDefault(x => x.Id == id);
    }

    public Director Update(Director entity)
    {
        entity.UpdatedTime = DateTime.UtcNow;
        _dbContext.Directors.Update(entity);
        _dbContext.SaveChanges();
        return entity;
    }
}


