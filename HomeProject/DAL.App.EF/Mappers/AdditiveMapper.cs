using AutoMapper;
using DALAppDTO = DAL.App.DTO;
using DomainApp = Domain.App;

namespace DAL.App.EF.Mappers
{
    public class AdditiveMapper: BaseMapper<DALAppDTO.Additive, DomainApp.Additive>
    {
        public AdditiveMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}
