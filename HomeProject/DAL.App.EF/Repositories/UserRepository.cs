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
    public class UserRepository : BaseRepository<DALAppDTO.User, DomainApp.User, AppDbContext>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new UserMapper(mapper))
        {
            
        }

        public async Task<IEnumerable<DALAppDTO.User>> GetAllWithCocktailsCountAsync(bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var resQuery = query.Select(u => new DALAppDTO.User
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                CocktailsCount = u.UsersWithCocktails!.Count
            });

            var result = await resQuery.ToListAsync();
            
            return result;
        }

        public async Task<DALAppDTO.User?> GetOneWithCocktailsCountAsync(Guid id, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var resQuery = query.Select(u => new DALAppDTO.User
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                CocktailsCount = u.UsersWithCocktails!.Count
            });

            var result = await resQuery.FirstOrDefaultAsync(e => e.Id == id);
            
            return result;
        }
    }
}
