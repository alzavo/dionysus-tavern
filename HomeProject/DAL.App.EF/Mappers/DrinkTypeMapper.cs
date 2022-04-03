using AutoMapper;
using DALAppDTO = DAL.App.DTO;
using DomainApp = Domain.App;

namespace DAL.App.EF.Mappers
{
    public class DrinkTypeMapper : BaseMapper<DALAppDTO.DrinkType, DomainApp.DrinkType>
    {
        public DrinkTypeMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}
