using AutoMapper;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class DrinkTypeMapper : BaseMapper<BLLAppDTO.DrinkType, DALAppDTO.DrinkType>
    {
        public DrinkTypeMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}
