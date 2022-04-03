using AutoMapper;
using DALAppDTO = DAL.App.DTO;
using DomainApp = Domain.App;

namespace DAL.App.EF.Mappers
{
    public class StepMapper : BaseMapper<DALAppDTO.Step, DomainApp.Step>
    {
        public StepMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}
