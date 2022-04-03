using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IUserWithCocktailRepository : IBaseRepository<DALAppDTO.UserWithCocktail>,
        IUserWithCocktailRepositoryCustom<DALAppDTO.UserWithCocktail>
    {
        
    }

    public interface IUserWithCocktailRepositoryCustom<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllDetailedAsync(Guid userId = default, bool noTracking = true);
        Task<TEntity?> GetOneDetailedAsync(Guid id, Guid userId = default, bool noTracking = true);
    }
}
