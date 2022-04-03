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
    public class AdditiveRepository : BaseRepository<DALAppDTO.Additive, DomainApp.Additive, AppDbContext>, IAdditiveRepository
    {
        public AdditiveRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new AdditiveMapper(mapper))
        {
        }

        public async Task<IEnumerable<DALAppDTO.Additive>> GetAllWithCocktailsCountAsync(bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var resQuery = query.Select(a => new DALAppDTO.Additive
                {
                    Id = a.Id,
                    Name = a.Name,
                    CocktailsCount = a.AdditivesInCocktails!.Count
                });

            var result = await resQuery.ToListAsync();
            
            return result;
        }

        public async Task<DALAppDTO.Additive?> GetOneWithCocktailsCountAsync(Guid id, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var resQuery = query.Select(a => new DALAppDTO.Additive
                {
                    Id = a.Id,
                    Name = a.Name,
                    CocktailsCount = a.AdditivesInCocktails!.Count
                });

            var result = await resQuery.FirstOrDefaultAsync(e => e.Id == id);
            
            return result;
        }
    }
}
