using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using DALAppDTO = DAL.App.DTO;
using DomainApp = Domain.App;

namespace DAL.App.EF.Repositories
{
    public class DrinkInCocktailRepository : BaseRepository<DALAppDTO.DrinkInCocktail, DomainApp.DrinkInCocktail, AppDbContext>, IDrinkInCocktailRepository
    {
        public DrinkInCocktailRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DrinkInCocktailMapper(mapper))
        {
            
        }

        public async Task<DALAppDTO.DrinkInCocktail?> GetOneDetailedAsync(Guid id, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var resQuery = query.Select(dic => new DALAppDTO.DrinkInCocktail
            {
                Id = dic.Id,
                CocktailId = dic.CocktailId,
                CocktailName = dic.Cocktail.Name,
                DrinkId = dic.DrinkId,
                DrinkName = dic.Drink.Name,
                AmountUnitId = dic.AmountUnitId,
                AmountUnitName = dic.AmountUnit.Name,
                Amount = dic.Amount
            });

            var result = await resQuery.FirstOrDefaultAsync(e => e.Id == id);

            return result;
        }
    }
}
