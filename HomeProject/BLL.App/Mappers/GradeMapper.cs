using AutoMapper;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class GradeMapper : BaseMapper<BLLAppDTO.Grade, DALAppDTO.Grade>
    {
        public GradeMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}
