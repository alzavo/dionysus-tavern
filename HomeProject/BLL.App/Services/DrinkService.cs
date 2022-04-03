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
    public class DrinkService : BaseEntityService<IAppUnitOfWork, IDrinkRepository, BLLAppDTO.Drink, DALAppDTO.Drink>,
        IDrinkService
    {
        public DrinkService(IAppUnitOfWork serviceUow, IDrinkRepository serviceRepository, IMapper mapper) : 
            base(serviceUow, serviceRepository, new DrinkMapper(mapper))
        {
        }

        public async Task<IEnumerable<BLLAppDTO.Drink>> GetAllWithDrinkTypeAndCocktailsCountAsync(bool noTracking = true)
        {
            return (await ServiceRepository.GetAllWithDrinkTypeAndCocktailsCountAsync(noTracking))
                .Select(d=> Mapper.Map(d))!;
        }

        public async Task<BLLAppDTO.Drink?> GetOneWithDrinkTypeAndCocktailsCountAsync(Guid id, bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.GetOneWithDrinkTypeAndCocktailsCountAsync(id, noTracking));
        }
    }
}