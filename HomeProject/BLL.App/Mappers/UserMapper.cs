using AutoMapper;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class UserMapper : BaseMapper<BLLAppDTO.User, DALAppDTO.User>
    {
        public UserMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}
