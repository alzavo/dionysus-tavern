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
    public class AdditiveService :
        BaseEntityService<IAppUnitOfWork, IAdditiveRepository, BLLAppDTO.Additive, DALAppDTO.Additive>, IAdditiveService
    {
        public AdditiveService(IAppUnitOfWork serviceUow, IAdditiveRepository serviceRepository, IMapper mapper) : 
            base(serviceUow, serviceRepository, new AdditiveMapper(mapper))
        {
        }

        public async Task<IEnumerable<BLLAppDTO.Additive>> GetAllWithCocktailsCountAsync(bool noTracking = true)
        {
            return (await ServiceRepository.GetAllWithCocktailsCountAsync(noTracking))
                .Select(a=> Mapper.Map(a))!;
        }

        public async Task<BLLAppDTO.Additive?> GetOneWithCocktailsCountAsync(Guid id, bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.GetOneWithCocktailsCountAsync(id, noTracking));
        }
    }
}
