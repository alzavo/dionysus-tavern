using AutoMapper;

namespace DAL.App.DTO.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DAL.App.DTO.Additive, Domain.App.Additive>().ReverseMap();
            CreateMap<DAL.App.DTO.AdditiveInCocktail, Domain.App.AdditiveInCocktail>().ReverseMap();
            CreateMap<DAL.App.DTO.AmountUnit, Domain.App.AmountUnit>().ReverseMap();
            CreateMap<DAL.App.DTO.Cocktail, Domain.App.Cocktail>().ReverseMap();
            CreateMap<DAL.App.DTO.Drink, Domain.App.Drink>().ReverseMap();
            CreateMap<DAL.App.DTO.DrinkInCocktail, Domain.App.DrinkInCocktail>().ReverseMap();
            CreateMap<DAL.App.DTO.DrinkType, Domain.App.DrinkType>().ReverseMap();
            CreateMap<DAL.App.DTO.Grade, Domain.App.Grade>().ReverseMap();
            CreateMap<DAL.App.DTO.Role, Domain.App.Role>().ReverseMap();
            CreateMap<DAL.App.DTO.Step, Domain.App.Step>().ReverseMap();
            CreateMap<DAL.App.DTO.User, Domain.App.User>().ReverseMap();
            CreateMap<DAL.App.DTO.UserWithCocktail, Domain.App.UserWithCocktail>().ReverseMap();
        }
    }
}
