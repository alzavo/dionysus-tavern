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
    public class DrinkTypeRepository : BaseRepository<DALAppDTO.DrinkType, DomainApp.DrinkType, AppDbContext>, IDrinkTypeRepository
    {
        public DrinkTypeRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DrinkTypeMapper(mapper)) 
        {
        }

        public async Task<IEnumerable<DALAppDTO.DrinkType>> GetAllWithDrinksCountAsync(bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var resultQuery = query.Select(dt=> new DALAppDTO.DrinkType
            {
                Id = dt.Id,
                Type = dt.Type,
                DrinksCount = dt.Drinks!.Count
            });

            var result = await resultQuery.ToListAsync();
        
            return result;
        }
        

        public async Task<DALAppDTO.DrinkType?> GetOneWithDrinksCountAsync(Guid id, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);
            
            var resultQuery = query.Select(dt=> new DALAppDTO.DrinkType
            {
                Id = dt.Id,
                Type = dt.Type,
                DrinksCount = dt.Drinks!.Count
            });
            
            var result = await resultQuery.FirstOrDefaultAsync(dt => dt.Id == id);

            return result;
        }
    } 
}  
