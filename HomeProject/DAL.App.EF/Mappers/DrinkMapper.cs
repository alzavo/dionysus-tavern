using AutoMapper;
using DALAppDTO = DAL.App.DTO;
using DomainApp = Domain.App;

namespace DAL.App.EF.Mappers
{
    public class DrinkMapper : BaseMapper<DALAppDTO.Drink, DomainApp.Drink>
    {
        public DrinkMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}
