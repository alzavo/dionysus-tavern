using BLLAppDTO = BLL.App.DTO;
using PublicApiDTOv1 = PublicApi.DTO.v1;

namespace PublicApi.DTO.v1.Mappers
{
    public static class DrinkMapper
    {
        public static BLLAppDTO.Drink MapToBLL(PublicApiDTOv1.DrinkCreate drinkCreate) 
        {
            return new()
            {
                Name = drinkCreate.Name,
                Abv = drinkCreate.Abv,
                DrinkTypeId = drinkCreate.DrinkTypeId
            };
        }
        
        public static BLLAppDTO.Drink MapToBLL(PublicApiDTOv1.DrinkUpdate drinkUpdate) 
        {
            return new()
            {
                Id = drinkUpdate.Id,
                DrinkTypeId = drinkUpdate.DrinkTypeId,
                Name = drinkUpdate.Name,
                Abv = drinkUpdate.Abv
            };
        }

        public static PublicApiDTOv1.Drink MapToPublic(BLLAppDTO.Drink drink)
        {
            return new()
            {
                Id = drink.Id,
                Name = drink.Name,
                Abv = drink.Abv,
                DrinkTypeId = drink.DrinkTypeId,
                DrinkType = drink.DrinkType,
                CocktailsCount = drink.CocktailsCount
            };
        }
    }
}
