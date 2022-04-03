using System;
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
    public class DrinkInCocktailService :
        BaseEntityService<IAppUnitOfWork, IDrinkInCocktailRepository, BLLAppDTO.DrinkInCocktail,
            DALAppDTO.DrinkInCocktail>, IDrinkInCocktailService
    {
        public DrinkInCocktailService(IAppUnitOfWork serviceUow, IDrinkInCocktailRepository serviceRepository,
            IMapper mapper) : base(serviceUow, serviceRepository, new DrinkInCocktailMapper(mapper))
        {
        }

        public async Task<BLLAppDTO.DrinkInCocktail?> GetOneDetailedAsync(Guid id, bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.GetOneDetailedAsync(id, noTracking));
        }
    }
}
