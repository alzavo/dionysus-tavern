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
    public class StepRepository : BaseRepository<DALAppDTO.Step, DomainApp.Step, AppDbContext>, IStepRepository
    {
        public StepRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new StepMapper(mapper))
        {
            
        }

        public async Task<DALAppDTO.Step?> GetOneWithCocktailNameAsync(Guid id, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var resQuery = query.Select(s => new DALAppDTO.Step
            {
                Id = s.Id,
                IndexNumber = s.IndexNumber,
                Description = s.Description,
                CocktailId = s.CocktailId,
                CocktailName = s.Cocktail.Name
            });

            var result = await resQuery.FirstOrDefaultAsync(e => e.Id == id);
            
            return result;
        }
    }
}
