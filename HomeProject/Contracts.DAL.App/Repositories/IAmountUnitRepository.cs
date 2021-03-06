using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IAmountUnitRepository : IBaseRepository<DALAppDTO.AmountUnit>, 
        IAmountUnitRepositoryCustom<DALAppDTO.AmountUnit>
    {
        
    }

    public interface IAmountUnitRepositoryCustom<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllWithUsageCountAsync(bool noTracking = true);
        Task<TEntity?> GetOneWithUsageCountAsync(Guid id, bool noTracking = true);
    }
}