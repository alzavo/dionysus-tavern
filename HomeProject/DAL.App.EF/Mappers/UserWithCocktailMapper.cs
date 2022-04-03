using AutoMapper;
using DALAppDTO = DAL.App.DTO;
using DomainApp = Domain.App;

namespace DAL.App.EF.Mappers
{
    public class UserWithCocktailMapper : BaseMapper<DALAppDTO.UserWithCocktail, DomainApp.UserWithCocktail>
    {
        public UserWithCocktailMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}
