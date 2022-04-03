using AutoMapper;

namespace BLL.App.DTO.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BLL.App.DTO.Additive, DAL.App.DTO.Additive>().ReverseMap();
            CreateMap<BLL.App.DTO.AdditiveInCocktail, DAL.App.DTO.AdditiveInCocktail>().ReverseMap();
            CreateMap<BLL.App.DTO.AmountUnit, DAL.App.DTO.AmountUnit>().ReverseMap();
            CreateMap<BLL.App.DTO.Cocktail, DAL.App.DTO.Cocktail>().ReverseMap();
            CreateMap<BLL.App.DTO.Drink, DAL.App.DTO.Drink>().ReverseMap();
            CreateMap<BLL.App.DTO.DrinkInCocktail, DAL.App.DTO.DrinkInCocktail>().ReverseMap();
            CreateMap<BLL.App.DTO.DrinkType, DAL.App.DTO.DrinkType>().ReverseMap();
            CreateMap<BLL.App.DTO.Grade, DAL.App.DTO.Grade>().ReverseMap();
            CreateMap<BLL.App.DTO.Role, DAL.App.DTO.Role>().ReverseMap();
            CreateMap<BLL.App.DTO.Step, DAL.App.DTO.Step>().ReverseMap();
            CreateMap<BLL.App.DTO.User, DAL.App.DTO.User>().ReverseMap();
            CreateMap<BLL.App.DTO.UserWithCocktail, DAL.App.DTO.UserWithCocktail>().ReverseMap();
        }
    }
}
