using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IUserRepository : IBaseRepository<DALAppDTO.User>,
        IUserRepositoryCustom<DALAppDTO.User>
    {

    }

    public interface IUserRepositoryCustom<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllWithCocktailsCountAsync(bool noTracking = true);
        Task<TEntity?> GetOneWithCocktailsCountAsync(Guid id, bool noTracking = true);
    }
}
