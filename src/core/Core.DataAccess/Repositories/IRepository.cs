using Core.DataAccess.Entities;

namespace Core.DataAccess.Repositories;

public interface IRepository<TEntity,TId> where TEntity: Entity<TId> 
{
    TEntity Add(TEntity entity);
    TEntity Update(TEntity entity);

    TEntity Delete(TEntity entity);

    TEntity? GetById(TId id);

    List<TEntity> GetAll();
}
