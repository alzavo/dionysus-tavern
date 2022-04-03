using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IStepRepository : IBaseRepository<DALAppDTO.Step>,
        IStepRepositoryCustom<DALAppDTO.Step>
    {
        
    }

    public interface IStepRepositoryCustom<TEntity>
    {
        Task<TEntity?> GetOneWithCocktailNameAsync(Guid id, bool noTracking = true);
    }
}
