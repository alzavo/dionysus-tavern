using AutoMapper;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class DrinkMapper : BaseMapper<BLLAppDTO.Drink, DALAppDTO.Drink>
    {
        public DrinkMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}
