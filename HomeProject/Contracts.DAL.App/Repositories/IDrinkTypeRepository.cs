using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IDrinkTypeRepository : IBaseRepository<DALAppDTO.DrinkType>, 
        IDrinkTypeRepositoryCustom<DALAppDTO.DrinkType>
    {
        
    }

    public interface IDrinkTypeRepositoryCustom<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllWithDrinksCountAsync(bool noTracking = true);
        Task<TEntity?> GetOneWithDrinksCountAsync(Guid id, bool noTracking = true);
    }
}
