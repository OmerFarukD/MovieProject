using Core.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Repositories
{
    public interface IAsyncRepository<TEntity,TId>
        where TEntity: Entity<TId>

    {
        Task<TEntity> AddAsync(TEntity entity,CancellationToken cancellationToken=default);
        Task<TEntity> UpdateAsync(TEntity entity,CancellationToken cancellationToken =default);

        Task<TEntity> DeleteAsync(TEntity entity,CancellationToken cancellationToken = default);


        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, bool include = true, bool enableTracking = true,CancellationToken cancellationToken=default);

        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter, bool include = true, bool enableTracking = true,CancellationToken cancellationToken=default);



        Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? filter = null, bool enableTracking = true,CancellationToken cancellationToken=default);



    }
}
