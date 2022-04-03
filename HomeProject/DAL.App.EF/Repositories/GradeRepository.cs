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
    public class GradeRepository : BaseRepository<DALAppDTO.Grade, DomainApp.Grade, AppDbContext>, IGradeRepository
    {
        public GradeRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new GradeMapper(mapper))
        {
        }

        public async Task<IEnumerable<DALAppDTO.Grade>> GetAllWithUsageCountAsync(bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var resQuery = query.Select(g => new DALAppDTO.Grade
            {
                Id = g.Id,
                GradeValue = g.GradeValue,
                UsageCount = g.UsersWithCocktails!.Count
            });

            var result = await resQuery.ToListAsync();
            
            return result;
        }

        public async Task<DALAppDTO.Grade?> GetOneWithUsageCountAsync(Guid id, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var resQuery = query.Select(g => new DALAppDTO.Grade
            {
                Id = g.Id,
                GradeValue = g.GradeValue,
                UsageCount = g.UsersWithCocktails!.Count
            });

            var result = await resQuery.FirstOrDefaultAsync(e => e.Id == id);
            
            return result;
        }
    }
}
