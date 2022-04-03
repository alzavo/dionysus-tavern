using AutoMapper;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class AdditiveMapper: BaseMapper<BLLAppDTO.Additive, DALAppDTO.Additive>
    {
        public AdditiveMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}
