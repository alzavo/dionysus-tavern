using BLLAppDTO = BLL.App.DTO;
using PublicApiDTOv1 = PublicApi.DTO.v1;

namespace PublicApi.DTO.v1.Mappers
{
    public static class DrinkTypeMapper
    {
        public static BLLAppDTO.DrinkType MapToBLL(PublicApiDTOv1.DrinkTypeCreate drinkTypeCreate) 
        {
            return new()
            {
                Type = drinkTypeCreate.Type
            };
        }
        
        public static BLLAppDTO.DrinkType MapToBLL(PublicApiDTOv1.DrinkTypeUpdate drinkTypeUpdate) 
        {
            return new()
            {
                Id = drinkTypeUpdate.Id,
                Type = drinkTypeUpdate.Type
            };
        }

        public static PublicApiDTOv1.DrinkType MapToPublic(BLLAppDTO.DrinkType drinkType)
        {
            return new()
            {
                Id = drinkType.Id,
                Type = drinkType.Type,
                DrinksCount = drinkType.DrinksCount
            };
        }
    }
}
