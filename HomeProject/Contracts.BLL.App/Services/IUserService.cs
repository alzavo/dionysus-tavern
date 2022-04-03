using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IUserService : IBaseEntityService<BLLAppDTO.User, DALAppDTO.User>, 
        IUserRepositoryCustom<BLLAppDTO.User>
    {
        
    }
}
