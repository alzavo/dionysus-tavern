using BLLAppDTO = BLL.App.DTO;
using PublicApiDTOv1 = PublicApi.DTO.v1;

namespace PublicApi.DTO.v1.Mappers
{
    public static class AdditiveMapper 
    {
        public static BLLAppDTO.Additive MapToBLL(PublicApiDTOv1.AdditiveCreate additiveCreate) 
        {
            return new()
            {
                Name = additiveCreate.Name
            };
        }
        
        public static BLLAppDTO.Additive MapToBLL(PublicApiDTOv1.AdditiveUpdate additiveUpdate) 
        {
            return new()
            {
                Id = additiveUpdate.Id,
                Name = additiveUpdate.Name
            };
        }

        public static PublicApiDTOv1.Additive MapToPublic(BLLAppDTO.Additive additive)
        {
            return new()
            {
                Id = additive.Id,
                Name = additive.Name,
                CocktailsCount = additive.CocktailsCount
            };
        }
    }
}
