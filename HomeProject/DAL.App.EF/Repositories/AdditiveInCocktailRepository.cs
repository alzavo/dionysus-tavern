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
    public class AdditiveInCocktailRepository : BaseRepository<DALAppDTO.AdditiveInCocktail, DomainApp.AdditiveInCocktail, AppDbContext>, IAdditiveInCocktailRepository
    {
        public AdditiveInCocktailRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new AdditiveInCocktailMapper(mapper))
        {
        }

        public async Task<DALAppDTO.AdditiveInCocktail?> GetOneDetailedAsync(Guid id, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var resQuery = query.Select(aic => new DALAppDTO.AdditiveInCocktail
            {
                Id = aic.Id,
                CocktailId = aic.CocktailId,
                CocktailName = aic.Cocktail.Name,
                AdditiveId = aic.AdditiveId,
                AdditiveName = aic.Additive.Name,
                AmountUnitId = aic.AmountUnitId,
                AmountUnitName = aic.AmountUnit.Name,
                Amount = aic.Amount
            });

            var result = await resQuery.FirstOrDefaultAsync(e => e.Id == id);
            
            return result;
        }
    }
}
