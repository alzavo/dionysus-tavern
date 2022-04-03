using AutoMapper;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class UserWithCocktailMapper : BaseMapper<BLLAppDTO.UserWithCocktail, DALAppDTO.UserWithCocktail>
    {
        public UserWithCocktailMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}
