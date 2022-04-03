using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IDrinkRepository : IBaseRepository<DALAppDTO.Drink>,
        IDrinkRepositoryCustom<DALAppDTO.Drink>
    {
    }

    public interface IDrinkRepositoryCustom<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllWithDrinkTypeAndCocktailsCountAsync(bool noTracking = true);
        Task<TEntity?> GetOneWithDrinkTypeAndCocktailsCountAsync(Guid id, bool noTracking = true);
    }
}