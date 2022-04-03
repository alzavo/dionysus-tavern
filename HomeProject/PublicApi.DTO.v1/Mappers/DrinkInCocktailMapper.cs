using BLLAppDTO = BLL.App.DTO;
using PublicApiDTOv1 = PublicApi.DTO.v1;

namespace PublicApi.DTO.v1.Mappers
{
    public static class DrinkInCocktailMapper
    {
        public static BLLAppDTO.DrinkInCocktail MapToBLL(PublicApiDTOv1.DrinkInCocktailCreate drinkInCocktailCreate) 
        {
            return new()
            {
                CocktailId = drinkInCocktailCreate.CocktailId,
                DrinkId = drinkInCocktailCreate.DrinkId,
                AmountUnitId = drinkInCocktailCreate.AmountUnitId,
                Amount = drinkInCocktailCreate.Amount
            };
        }
        
        public static BLLAppDTO.DrinkInCocktail MapToBLL(PublicApiDTOv1.DrinkInCocktailUpdate drinkInCocktailUpdate) 
        {
            return new()
            {
                Id = drinkInCocktailUpdate.Id,
                CocktailId = drinkInCocktailUpdate.CocktailId,
                DrinkId = drinkInCocktailUpdate.DrinkId,
                AmountUnitId = drinkInCocktailUpdate.AmountUnitId,
                Amount = drinkInCocktailUpdate.Amount
            };
        }

        public static PublicApiDTOv1.DrinkInCocktail MapToPublic(BLLAppDTO.DrinkInCocktail drinkInCocktail)
        {
            return new()
            {
                Id = drinkInCocktail.Id,
                CocktailId = drinkInCocktail.CocktailId,
                CocktailName = drinkInCocktail.CocktailName,
                DrinkId = drinkInCocktail.DrinkId,
                DrinkName = drinkInCocktail.DrinkName,
                AmountUnitId = drinkInCocktail.AmountUnitId,
                AmountUnitName = drinkInCocktail.AmountUnitName,
                Amount = drinkInCocktail.Amount
            };
        }
    }
}
