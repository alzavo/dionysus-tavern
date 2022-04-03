using BLLAppDTO = BLL.App.DTO;
using PublicApiDTOv1 = PublicApi.DTO.v1;

namespace PublicApi.DTO.v1.Mappers
{
    public static class AdditiveInCocktailMapper
    {
        public static BLLAppDTO.AdditiveInCocktail MapToBLL(PublicApiDTOv1.AdditiveInCocktailCreate additiveInCocktailCreate) 
        {
            return new()
            {
                CocktailId = additiveInCocktailCreate.CocktailId,
                AdditiveId = additiveInCocktailCreate.AdditiveId,
                AmountUnitId = additiveInCocktailCreate.AmountUnitId,
                Amount = additiveInCocktailCreate.Amount
            };
        }
        
        public static BLLAppDTO.AdditiveInCocktail MapToBLL(PublicApiDTOv1.AdditiveInCocktailUpdate additiveInCocktailUpdate) 
        {
            return new()
            {
                Id = additiveInCocktailUpdate.Id,
                CocktailId = additiveInCocktailUpdate.CocktailId,
                AdditiveId = additiveInCocktailUpdate.AdditiveId,
                AmountUnitId = additiveInCocktailUpdate.AmountUnitId,
                Amount = additiveInCocktailUpdate.Amount
            };
        }

        public static PublicApiDTOv1.AdditiveInCocktail MapToPublic(BLLAppDTO.AdditiveInCocktail additiveInCocktail)
        {
            return new()
            {
                Id = additiveInCocktail.Id,
                CocktailId = additiveInCocktail.CocktailId,
                CocktailName = additiveInCocktail.CocktailName,
                AdditiveId = additiveInCocktail.AdditiveId,
                AdditiveName = additiveInCocktail.AdditiveName,
                AmountUnitId = additiveInCocktail.AmountUnitId,
                AmountUnitName = additiveInCocktail.AmountUnitName,
                Amount = additiveInCocktail.Amount
            };
        }
    }
}
