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
    public class StepService : BaseEntityService<IAppUnitOfWork, IStepRepository, BLLAppDTO.Step, DALAppDTO.Step>,
        IStepService
    {
        public StepService(IAppUnitOfWork serviceUow, IStepRepository serviceRepository, IMapper mapper) : 
            base(serviceUow, serviceRepository, new StepMapper(mapper))
        {
            
        }

        public async Task<BLLAppDTO.Step?> GetOneWithCocktailNameAsync(Guid id, bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.GetOneWithCocktailNameAsync(id, noTracking));
        }
    }
}
