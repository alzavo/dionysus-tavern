using AutoMapper;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class AdditiveInCocktailMapper : BaseMapper<BLLAppDTO.AdditiveInCocktail, DALAppDTO.AdditiveInCocktail>
    {
        public AdditiveInCocktailMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}
