using BLLAppDTO = BLL.App.DTO;
using PublicApiDTOv1 = PublicApi.DTO.v1;

namespace PublicApi.DTO.v1.Mappers
{
    public static class StepMapper
    {
        public static BLLAppDTO.Step MapToBLL(PublicApiDTOv1.StepCreate stepCreate) 
        {
            return new()
            {
                IndexNumber = stepCreate.IndexNumber,
                Description = stepCreate.Description,
                CocktailId = stepCreate.CocktailId
            };
        }
        
        public static BLLAppDTO.Step MapToBLL(PublicApiDTOv1.StepUpdate stepUpdate) 
        {
            return new()
            {
                Id = stepUpdate.Id,
                IndexNumber = stepUpdate.IndexNumber,
                Description = stepUpdate.Description,
                CocktailId = stepUpdate.CocktailId
            };
        }

        public static PublicApiDTOv1.Step MapToPublic(BLLAppDTO.Step step)
        {
            return new()
            {
                Id = step.Id,
                IndexNumber = step.IndexNumber,
                Description = step.Description,
                CocktailId = step.CocktailId,
                CocktailName = step.CocktailName
            };
        }
    }
}
