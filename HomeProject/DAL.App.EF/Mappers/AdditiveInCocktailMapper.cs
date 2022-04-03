using AutoMapper;
using DALAppDTO = DAL.App.DTO;
using DomainApp = Domain.App;

namespace DAL.App.EF.Mappers
{
    public class AdditiveInCocktailMapper : BaseMapper<DALAppDTO.AdditiveInCocktail, DomainApp.AdditiveInCocktail>
    {
        public AdditiveInCocktailMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}
