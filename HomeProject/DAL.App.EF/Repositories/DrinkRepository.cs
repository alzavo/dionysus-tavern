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
    public class DrinkRepository : BaseRepository<DALAppDTO.Drink, DomainApp.Drink, AppDbContext>, IDrinkRepository
    {
        public DrinkRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DrinkMapper(mapper))
        {
        }
        
        public async Task<IEnumerable<DALAppDTO.Drink>> GetAllWithDrinkTypeAndCocktailsCountAsync(bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var resQuery = query.Select(d => new DALAppDTO.Drink
                {
                    Id = d.Id,
                    Name = d.Name,
                    Abv = d.Abv,
                    DrinkTypeId = d.DrinkTypeId,
                    DrinkType = d.DrinkType.Type,
                    CocktailsCount = d.DrinksInCocktails!.Count
                });

            var result = await resQuery.ToListAsync();
            
            return result;
        }

        public async Task<DALAppDTO.Drink?> GetOneWithDrinkTypeAndCocktailsCountAsync(Guid id, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var resQuery = query.Select(d => new DALAppDTO.Drink
            {
                Id = d.Id,
                Name = d.Name,
                Abv = d.Abv,
                DrinkTypeId = d.DrinkTypeId,
                DrinkType = d.DrinkType.Type,
                CocktailsCount = d.DrinksInCocktails!.Count
            });

            var result = await resQuery.FirstOrDefaultAsync(e => e.Id == id);
            
            return result;
        }
    }
}
