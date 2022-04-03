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
    public class GradeService : BaseEntityService<IAppUnitOfWork, IGradeRepository, BLLAppDTO.Grade, DALAppDTO.Grade>,
        IGradeService
    {
        public GradeService(IAppUnitOfWork serviceUow, IGradeRepository serviceRepository, IMapper mapper) : 
            base(serviceUow, serviceRepository, new GradeMapper(mapper))
        {
        }

        public async Task<IEnumerable<BLLAppDTO.Grade>> GetAllWithUsageCountAsync(bool noTracking = true)
        {
            return (await ServiceRepository.GetAllWithUsageCountAsync(noTracking))
                .Select(g => Mapper.Map(g))!;
        }

        public async Task<BLLAppDTO.Grade?> GetOneWithUsageCountAsync(Guid id, bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.GetOneWithUsageCountAsync(id, noTracking));
        }
    }
}
