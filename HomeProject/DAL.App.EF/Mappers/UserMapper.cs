using AutoMapper;
using DALAppDTO = DAL.App.DTO;
using DomainApp = Domain.App;

namespace DAL.App.EF.Mappers
{
    public class UserMapper : BaseMapper<DALAppDTO.User, DomainApp.User>
    {
        public UserMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}
