using AutoMapper;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class StepMapper : BaseMapper<BLLAppDTO.Step, DALAppDTO.Step>
    {
        public StepMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}
