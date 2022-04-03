using AutoMapper;
using DALAppDTO = DAL.App.DTO;
using DomainApp = Domain.App;

namespace DAL.App.EF.Mappers
{
    public class DrinkInCocktailMapper : BaseMapper<DALAppDTO.DrinkInCocktail, DomainApp.DrinkInCocktail>
    {
        public DrinkInCocktailMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}
