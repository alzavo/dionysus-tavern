using System;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IAdditiveInCocktailRepository : IBaseRepository<DALAppDTO.AdditiveInCocktail>,
        IAdditiveInCocktailRepositoryCustom<DALAppDTO.AdditiveInCocktail>
    {
        
    }
    
    public interface IAdditiveInCocktailRepositoryCustom<TEntity>
    {
        Task<TEntity?> GetOneDetailedAsync(Guid id, bool noTracking = true);
    }
}
