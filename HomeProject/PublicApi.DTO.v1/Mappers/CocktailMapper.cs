using System.Linq;
using BLLAppDTO = BLL.App.DTO;
using PublicApiDTOv1 = PublicApi.DTO.v1;

namespace PublicApi.DTO.v1.Mappers
{
    public static class CocktailMapper
    {
        public static BLLAppDTO.Cocktail MapToBLL(PublicApiDTOv1.CocktailUpdate cocktail)
        {
            return new()
            {
                Id = cocktail.Id,
                Name = cocktail.Name,
                Alcoholic = cocktail.Alcoholic
            };
        }
        
        public static BLLAppDTO.Cocktail MapToBLL(PublicApiDTOv1.CocktailCreate cocktail)
        {
            return new()
            {
                Name = cocktail.Name,
                Alcoholic = cocktail.Alcoholic
            };
        }
        
        public static PublicApiDTOv1.Cocktail MapToPublic(BLLAppDTO.Cocktail cocktail)
        {
            return new()
            {
                Id = cocktail.Id,
                Name = cocktail.Name,
                Alcoholic = cocktail.Alcoholic,
                IngredientsCount = cocktail.IngredientsCount,
                StepsCount = cocktail.StepsCount,
                UsersCount = cocktail.UsersCount
            };
        }
        
        public static PublicApiDTOv1.CocktailDetailed MapToDetailedPublic(BLLAppDTO.Cocktail cocktail)
        {
            return new()
            {
                Id = cocktail.Id,
                Name = cocktail.Name,
                Alcoholic = cocktail.Alcoholic,
                UsersCount = cocktail.UsersCount,
                DrinksInCocktail = cocktail.DrinksInCocktails!.Select(din=> new DrinkInCocktail
                {
                    Id = din.Id,
                    CocktailId = din.CocktailId,
                    DrinkId = din.DrinkId,
                    DrinkName = din.DrinkName,
                    AmountUnitId = din.AmountUnitId,
                    AmountUnitName = din.AmountUnitName,
                    Amount = din.Amount 
                }).ToList(),
                AdditivesInCocktail = cocktail.AdditivesInCocktails!.Select(ain=> new AdditiveInCocktail
                {
                    Id = ain.Id,
                    CocktailId = ain.CocktailId,
                    AdditiveId = ain.AdditiveId,
                    AdditiveName = ain.AdditiveName,
                    AmountUnitId = ain.AmountUnitId,
                    AmountUnitName = ain.AmountUnitName,
                    Amount = ain.Amount
                }).ToList(),
                Steps = cocktail.Steps!.Select(s=> new Step
                {
                    Id = s.Id,
                    CocktailId = s.CocktailId,
                    Description = s.Description,
                    IndexNumber = s.IndexNumber
                }).ToList()
            };
        }
    }
}
