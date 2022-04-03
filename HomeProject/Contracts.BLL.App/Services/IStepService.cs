using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IStepService : IBaseEntityService<BLLAppDTO.Step, DALAppDTO.Step>, 
        IStepRepositoryCustom<BLLAppDTO.Step>
    {
        
    }
}