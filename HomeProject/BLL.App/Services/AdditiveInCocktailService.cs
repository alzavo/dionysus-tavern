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
    public class AdditiveInCocktailService :
        BaseEntityService<IAppUnitOfWork, IAdditiveInCocktailRepository, BLLAppDTO.AdditiveInCocktail,
            DALAppDTO.AdditiveInCocktail>, IAdditiveInCocktailService
    {
        public AdditiveInCocktailService(IAppUnitOfWork serviceUow, IAdditiveInCocktailRepository serviceRepository,
            IMapper mapper) :
            base(serviceUow, serviceRepository, new AdditiveInCocktailMapper(mapper))
        {
        }

        public async Task<BLLAppDTO.AdditiveInCocktail?> GetOneDetailedAsync(Guid id, bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.GetOneDetailedAsync(id, noTracking));
        }
    }
}
