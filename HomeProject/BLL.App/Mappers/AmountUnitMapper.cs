using AutoMapper;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class AmountUnitMapper : BaseMapper<BLLAppDTO.AmountUnit, DALAppDTO.AmountUnit>
    {
        public AmountUnitMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}
