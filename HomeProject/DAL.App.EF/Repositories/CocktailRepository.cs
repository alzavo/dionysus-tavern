using System;
using System.Collections.Generic;
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
    public class CocktailRepository : BaseRepository<DALAppDTO.Cocktail, DomainApp.Cocktail, AppDbContext>, ICocktailRepository
    {
        public CocktailRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new CocktailMapper(mapper))
        {
            
        }

        public async Task<IEnumerable<DALAppDTO.Cocktail>> GetAllWithSmallOverviewAsync(bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var resQuery = query.Select(c=> new DALAppDTO.Cocktail
            {
                Id = c.Id,
                Name = c.Name,
                Alcoholic = c.Alcoholic,
                IngredientsCount = c.AdditivesInCocktails!.Count + c.DrinksInCocktails!.Count,
                StepsCount = c.Steps!.Count,
                UsersCount = c.UsersWithCocktails!.Count
            });

            var result = await resQuery.ToListAsync();
            
            return result;
        }

        public async Task<DALAppDTO.Cocktail?> GetOneWithFullInfoAsync(Guid id, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var resQuery = query.Select(c => new DALAppDTO.Cocktail
            {
                Id = c.Id,
                Name = c.Name,
                Alcoholic = c.Alcoholic,
                UsersCount = c.UsersWithCocktails!.Count,
                DrinksInCocktails = c.DrinksInCocktails!.Select(din=> new DALAppDTO.DrinkInCocktail
                {
                    Id = din.Id,
                    CocktailId = din.CocktailId,
                    DrinkId = din.DrinkId,
                    DrinkName = din.Drink.Name,
                    AmountUnitId = din.AmountUnitId,
                    AmountUnitName = din.AmountUnit.Name,
                    Amount = din.Amount
                }).ToList(),
                AdditivesInCocktails = c.AdditivesInCocktails!.Select(ain=> new DALAppDTO.AdditiveInCocktail
                {
                    Id = ain.Id,
                    CocktailId = ain.CocktailId,
                    AdditiveId = ain.AdditiveId,
                    AdditiveName = ain.Additive.Name,
                    AmountUnitId = ain.AmountUnitId,
                    AmountUnitName = ain.AmountUnit.Name,
                    Amount = ain.Amount
                }).ToList(),
                Steps = c.Steps!.Select(s=> new DALAppDTO.Step
                {
                    Id = s.Id,
                    CocktailId = s.CocktailId,
                    Description = s.Description,
                    IndexNumber = s.IndexNumber
                }).ToList()
            });

            var result = await resQuery.FirstOrDefaultAsync(e => e.Id == id);
            
            return result;
        }
    }
}
