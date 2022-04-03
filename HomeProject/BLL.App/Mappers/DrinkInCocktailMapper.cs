using AutoMapper;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class DrinkInCocktailMapper : BaseMapper<BLLAppDTO.DrinkInCocktail, DALAppDTO.DrinkInCocktail>
    {
        public DrinkInCocktailMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}
