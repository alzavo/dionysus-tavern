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
    public class CocktailService :
        BaseEntityService<IAppUnitOfWork, ICocktailRepository, BLLAppDTO.Cocktail, DALAppDTO.Cocktail>, ICocktailService
    {
        public CocktailService(IAppUnitOfWork serviceUow, ICocktailRepository serviceRepository, IMapper mapper) : 
            base(serviceUow, serviceRepository, new CocktailMapper(mapper))
        {
        }

        public async Task<IEnumerable<BLLAppDTO.Cocktail>> GetAllWithSmallOverviewAsync(bool noTracking = true)
        {
            return (await ServiceRepository.GetAllWithSmallOverviewAsync(noTracking))
                .Select(a=> Mapper.Map(a))!;
        }

        public async Task<BLLAppDTO.Cocktail?> GetOneWithFullInfoAsync(Guid id, bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.GetOneWithFullInfoAsync(id, noTracking));
        }
    }
}
