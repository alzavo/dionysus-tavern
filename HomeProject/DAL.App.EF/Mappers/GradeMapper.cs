using AutoMapper;
using DALAppDTO = DAL.App.DTO;
using DomainApp = Domain.App;

namespace DAL.App.EF.Mappers
{
    public class GradeMapper : BaseMapper<DALAppDTO.Grade, DomainApp.Grade>
    {
        public GradeMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}
