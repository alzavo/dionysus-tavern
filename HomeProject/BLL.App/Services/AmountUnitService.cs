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
    public class AmountUnitService :
        BaseEntityService<IAppUnitOfWork, IAmountUnitRepository, BLLAppDTO.AmountUnit, DALAppDTO.AmountUnit>,
        IAmountUnitService
    {
        public AmountUnitService(IAppUnitOfWork serviceUow, IAmountUnitRepository serviceRepository, IMapper mapper) :
            base(serviceUow, serviceRepository, new AmountUnitMapper(mapper))
        {
        }

        public async Task<IEnumerable<BLLAppDTO.AmountUnit>> GetAllWithUsageCountAsync(bool noTracking = true)
        {
            return (await ServiceRepository.GetAllWithUsageCountAsync(noTracking))
                .Select(au => Mapper.Map(au))!;
        }

        public async Task<BLLAppDTO.AmountUnit?> GetOneWithUsageCountAsync(Guid id, bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.GetOneWithUsageCountAsync(id, noTracking));
        }
    }
}
