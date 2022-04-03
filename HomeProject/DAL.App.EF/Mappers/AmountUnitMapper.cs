using AutoMapper;
using DALAppDTO = DAL.App.DTO;
using DomainApp = Domain.App;

namespace DAL.App.EF.Mappers
{
    public class AmountUnitMapper : BaseMapper<DALAppDTO.AmountUnit, DomainApp.AmountUnit>
    {
        public AmountUnitMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}
