using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Domain.Base;

namespace Contracts.DAL.Base
{
    public interface IBaseRepository<TEntity> : IBaseRepository<TEntity, Guid>
        where TEntity : class, IDomainEntityId<Guid>
    {
    }

    public interface IBaseRepository<TEntity, TKey>
        where TEntity : class, IDomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(TKey? userId = default, bool noTracking = true);
        
        Task<TEntity?> FirstOrDefaultAsync(TKey id, TKey? userId = default, bool noTracking = true);

        TEntity Add(TEntity entity);
        
        TEntity Update(TEntity entity);

        TEntity Remove(TEntity entity, TKey? userId = default);  

        Task<TEntity> RemoveAsync(TKey id, TKey? userId = default);

        Task<bool> ExistsAsync(TKey id, TKey? userId = default);
    }
}