using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICocktailRepository : IBaseRepository<DALAppDTO.Cocktail>,
        ICocktailRepositoryCustom<DALAppDTO.Cocktail>
    {
        
    }

    public interface ICocktailRepositoryCustom<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllWithSmallOverviewAsync(bool noTracking = true);
        Task<TEntity?> GetOneWithFullInfoAsync(Guid id, bool noTracking = true);
    }
}
