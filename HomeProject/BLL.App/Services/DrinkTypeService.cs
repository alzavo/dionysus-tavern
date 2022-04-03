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
    public class DrinkTypeService : BaseEntityService<IAppUnitOfWork, IDrinkTypeRepository, BLLAppDTO.DrinkType, DALAppDTO.DrinkType>, IDrinkTypeService
    {
        public DrinkTypeService(IAppUnitOfWork serviceUow, IDrinkTypeRepository serviceRepository, IMapper mapper) : 
            base(serviceUow, serviceRepository, new DrinkTypeMapper(mapper))
        {
        }

        public async Task<IEnumerable<BLLAppDTO.DrinkType>> GetAllWithDrinksCountAsync(bool noTracking = true)
        {
            return (await ServiceRepository.GetAllWithDrinksCountAsync(noTracking))
                .Select(dt=> Mapper.Map(dt))!;
        }

        public async Task<BLLAppDTO.DrinkType?> GetOneWithDrinksCountAsync(Guid id, bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.GetOneWithDrinksCountAsync(id, noTracking));
        }
    }
}
