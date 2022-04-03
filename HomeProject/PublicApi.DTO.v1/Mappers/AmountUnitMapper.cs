using BLLAppDTO = BLL.App.DTO;
using PublicApiDTOv1 = PublicApi.DTO.v1;

namespace PublicApi.DTO.v1.Mappers
{
    public class AmountUnitMapper
    {
        public static BLLAppDTO.AmountUnit MapToBLL(PublicApiDTOv1.AmountUnitCreate amountUnitCreate) 
        {
            return new()
            {
                Name = amountUnitCreate.Name
            };
        }
        
        public static BLLAppDTO.AmountUnit MapToBLL(PublicApiDTOv1.AmountUnitUpdate amountUnitUpdate) 
        {
            return new()
            {
                Id = amountUnitUpdate.Id,
                Name = amountUnitUpdate.Name
            };
        }

        public static PublicApiDTOv1.AmountUnit MapToPublic(BLLAppDTO.AmountUnit amountUnit)
        {
            return new()
            {
                Id = amountUnit.Id,
                Name = amountUnit.Name,
                UsageCount = amountUnit.UsageCount
            };
        }
    }
}
