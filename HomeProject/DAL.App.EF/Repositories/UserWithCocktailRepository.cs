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
    public class UserWithCocktailRepository : BaseRepository<DALAppDTO.UserWithCocktail, DomainApp.UserWithCocktail, AppDbContext>, IUserWithCocktailRepository
    {
        public UserWithCocktailRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new UserWithCocktailMapper(mapper))
        {
            
        }
        
        public async Task<IEnumerable<DALAppDTO.UserWithCocktail>> GetAllDetailedAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            var resQuery = query.Select(uwc => new DALAppDTO.UserWithCocktail
            {
                Id = uwc.Id,
                UserId = uwc.UserId,
                CocktailId = uwc.CocktailId,
                CocktailName = uwc.Cocktail.Name,
                GradeId = uwc.GradeId,
                GradeValue = uwc.Grade.GradeValue,
                Comment = uwc.Comment
            });

            var result = await resQuery.ToListAsync();
            
            return result;
        }

        public async Task<DALAppDTO.UserWithCocktail?> GetOneDetailedAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            var resQuery = query.Select(uwc => new DALAppDTO.UserWithCocktail
            {
                Id = uwc.Id,
                UserId = uwc.UserId,
                CocktailId = uwc.CocktailId,
                CocktailName = uwc.Cocktail.Name,
                GradeId = uwc.GradeId,
                GradeValue = uwc.Grade.GradeValue,
                Comment = uwc.Comment
            });

            var result = await resQuery.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            return result;
        }
    }
}
