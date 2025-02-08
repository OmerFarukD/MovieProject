using Core.DataAccess.Entities;
using System.Linq.Expressions;

namespace Core.DataAccess.Repositories;

public interface IRepository<TEntity,TId> where TEntity: Entity<TId> 
{
    TEntity Add(TEntity entity);
    TEntity Update(TEntity entity);

    TEntity Delete(TEntity entity);

    TEntity? GetById(TId id);

    List<TEntity> GetAll(Expression<Func<TEntity,bool>>? filter =null,bool include = true,bool enableTracking=true);

    TEntity? Get(Expression<Func<TEntity, bool>> filter , bool include = true, bool enableTracking = true);


    bool Any(Expression<Func<TEntity, bool>>? filter = null, bool enableTracking = true);

    IQueryable<TEntity> Query();
}
