using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Mappers;
using Contracts.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF.Repositories
{
    public class BaseRepository<TDalEntity, TDomainEntity, TDbContext> :
        BaseRepository<TDalEntity, TDomainEntity, Guid, TDbContext>, 
        IBaseRepository<TDalEntity> 
        where TDalEntity : class, IDomainEntityId
        where TDomainEntity : class, IDomainEntityId
        where TDbContext : DbContext 
    {
        public BaseRepository(TDbContext dbContext, IBaseMapper<TDalEntity, TDomainEntity> mapper) : base(dbContext, mapper)
        {
        }
    }
    
    public class BaseRepository<TDalEntity, TDomainEntity, TKey, TDbContext> : IBaseRepository<TDalEntity, TKey>
        where TDalEntity : class, IDomainEntityId<TKey>
        where TDomainEntity : class, IDomainEntityId<TKey>
        where TKey : IEquatable<TKey>
        where TDbContext : DbContext
    {
        protected readonly TDbContext RepoDbContext;
        protected readonly DbSet<TDomainEntity> RepoDbSet;
        protected readonly IBaseMapper<TDalEntity, TDomainEntity> Mapper;
        public BaseRepository(TDbContext dbContext, IBaseMapper<TDalEntity, TDomainEntity> mapper)
        {
            RepoDbContext = dbContext;
            Mapper = mapper;
            RepoDbSet = dbContext.Set<TDomainEntity>();
        }

        protected IQueryable<TDomainEntity> CreateQuery(TKey? userId = default, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            // TODO: validate the input entity also
            if (userId != null && !userId.Equals(default) && 
                typeof(IDomainUserId<TKey>).IsAssignableFrom(typeof(TDomainEntity))) 
            {
                // ReSharper disable once SuspiciousTypeConversion.Global
                query = query.Where(e => ((IDomainUserId<TKey>) e).UserId.Equals(userId));
            }
            
            if (noTracking) query = query.AsNoTracking();

            return query;
        }
        
        public virtual async Task<IEnumerable<TDalEntity>> GetAllAsync(TKey? userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            var resultQuery = query.Select(domainEntity => Mapper.Map(domainEntity));
            var result = await resultQuery.ToListAsync();

            return result!;
        }

        public virtual async Task<TDalEntity?> FirstOrDefaultAsync(TKey id, TKey? userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            
            return Mapper.Map(await query.FirstOrDefaultAsync(e => e!.Id.Equals(id)));
        }

        public virtual TDalEntity Add(TDalEntity entity)
        {
            return Mapper.Map(RepoDbSet.Add(Mapper.Map(entity)!).Entity)!; 
        }

        public virtual TDalEntity Update(TDalEntity entity)
        {
            return Mapper.Map(RepoDbSet.Update(Mapper.Map(entity)!).Entity)!; 
        }

        public virtual TDalEntity Remove(TDalEntity entity, TKey? userId = default)
        {
            if (userId != null && !userId.Equals(default) && 
                typeof(IDomainUserId<TKey>).IsAssignableFrom(typeof(TDomainEntity)) && 
                !((IDomainUserId<TKey>) entity).UserId.Equals(userId))
            {
                throw new AuthenticationException($"Bad user id inside entity {typeof(TDalEntity).Name} to be deleted");
                // TODO: load entity from the db, check that userId inside entity is correct
            }
            
            return Mapper.Map(RepoDbSet.Remove(Mapper.Map(entity)!).Entity)!; 
        }
        
        public virtual async Task<TDalEntity> RemoveAsync(TKey id, TKey? userId = default)
        {
            var entity = await FirstOrDefaultAsync(id, userId);
            if (entity == null) throw new NullReferenceException($"Entity {typeof(TDalEntity).Name} with id {id} is not found!");
            return Remove(entity, userId);
        }

        public virtual async Task<bool> ExistsAsync(TKey id, TKey? userId = default)
        {
            if (userId == null || userId.Equals(default))
            {
                return await RepoDbSet.AnyAsync(e => e.Id.Equals(id));
            }

            if (!typeof(IDomainUserId<TKey>).IsAssignableFrom(typeof(TDomainEntity)))
            {
                throw new AuthenticationException(
                    $"Entity {typeof(TDomainEntity).Name} does not implement required interface: {typeof(IDomainUserId<TKey>).Name} for UserId check");
            }
            
            return await RepoDbSet.AnyAsync(e => e.Id.Equals(id) && ((IDomainUserId<TKey>)e).UserId.Equals(userId));
        }
    }
}