using AutoMapper;
using DALAppDTO = DAL.App.DTO;
using DomainApp = Domain.App;

namespace DAL.App.EF.Mappers 
{
    public class CocktailMapper : BaseMapper<DALAppDTO.Cocktail, DomainApp.Cocktail>
    {
        public CocktailMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}
