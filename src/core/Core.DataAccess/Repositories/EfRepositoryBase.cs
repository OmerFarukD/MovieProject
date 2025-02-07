
using Core.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
namespace Core.DataAccess.Repositories;

public abstract class EfRepositoryBase<TEntity, TId, TContext> : IRepository<TEntity, TId>
    where TEntity: Entity<TId>
    where TContext: DbContext

{
    protected  TContext Context { get;}


    public EfRepositoryBase(TContext context)
    {
        Context = context;
    }
    public TEntity Add(TEntity entity)
    {

        entity.CreatedTime = DateTime.Now;
        Context.Set<TEntity>().Add(entity);
        Context.SaveChanges();
        return entity;
    }

    public TEntity Delete(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
        Context.SaveChanges();
        return entity;
    }

    public List<TEntity> GetAll(bool include =true)
    {
        // SELECT [] FROM TENTITY WHERE [] ORDER BY []

        IQueryable<TEntity> query = Context.Set<TEntity>();

        if(include is false)
        {
            query = query.IgnoreAutoIncludes();
        }

        return query.ToList();
    }

    public TEntity? GetById(TId id)
    {
        return Context.Set<TEntity>().Find(id);
    }

    public TEntity Update(TEntity entity)
    {
        entity.UpdatedTime = DateTime.Now;
        Context.Set<TEntity>().Update(entity);
        Context.SaveChanges();
        return entity;
    }

    public IQueryable<TEntity> Query()
    {
       return Context.Set<TEntity>();
    }
}
