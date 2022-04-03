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
    public class AmountUnitRepository : BaseRepository<DALAppDTO.AmountUnit, DomainApp.AmountUnit, AppDbContext>, IAmountUnitRepository
    {
        public AmountUnitRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new AmountUnitMapper(mapper))
        {
        }

        public async Task<IEnumerable<DALAppDTO.AmountUnit>> GetAllWithUsageCountAsync(bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var resQuery = query.Select(au => new DALAppDTO.AmountUnit
            {
                Id = au.Id,
                Name = au.Name,
                UsageCount = au.AdditivesInCocktails!.Count + au.DrinkInCocktails!.Count
            });

            var result = await resQuery.ToListAsync();
            
            return result;
        }

        public async Task<DALAppDTO.AmountUnit?> GetOneWithUsageCountAsync(Guid id, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var resQuery = query.Select(au => new DALAppDTO.AmountUnit
            {
                Id = au.Id,
                Name = au.Name,
                UsageCount = au.AdditivesInCocktails!.Count + au.DrinkInCocktails!.Count
            });

            var result = await resQuery.FirstOrDefaultAsync();
            
            return result;
        }
    }
}
