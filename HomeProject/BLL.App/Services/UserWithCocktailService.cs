using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class UserWithCocktailService :
        BaseEntityService<IAppUnitOfWork, IUserWithCocktailRepository, BLLAppDTO.UserWithCocktail,
            DALAppDTO.UserWithCocktail>, IUserWithCocktailService
    {
        public UserWithCocktailService(IAppUnitOfWork serviceUow, IUserWithCocktailRepository serviceRepository,
            IMapper mapper) : base(serviceUow, serviceRepository, new UserWithCocktailMapper(mapper))
        {
        }

        public async Task<IEnumerable<BLLAppDTO.UserWithCocktail>> GetAllDetailedAsync(Guid userId = default, bool noTracking = true)
        {
            return (await ServiceRepository.GetAllDetailedAsync(userId, noTracking))
                .Select(a=> Mapper.Map(a))!;
        }

        public async Task<BLLAppDTO.UserWithCocktail?> GetOneDetailedAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.GetOneDetailedAsync(id, userId, noTracking));
        }
    }
}
