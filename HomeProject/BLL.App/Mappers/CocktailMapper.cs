using AutoMapper;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Mappers 
{
    public class CocktailMapper : BaseMapper<BLLAppDTO.Cocktail, DALAppDTO.Cocktail>
    {
        public CocktailMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}
