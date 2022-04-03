using System;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IDrinkInCocktailRepository : IBaseRepository<DALAppDTO.DrinkInCocktail>,
        IDrinkInCocktailRepositoryCustom<DALAppDTO.DrinkInCocktail>
    {
        
    }

    public interface IDrinkInCocktailRepositoryCustom<TEntity>
    {
        Task<TEntity?> GetOneDetailedAsync(Guid id, bool noTracking = true);
    }
}
