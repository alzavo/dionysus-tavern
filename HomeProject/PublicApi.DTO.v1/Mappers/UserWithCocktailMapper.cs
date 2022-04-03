using BLLAppDTO = BLL.App.DTO;
using PublicApiDTOv1 = PublicApi.DTO.v1;

namespace PublicApi.DTO.v1.Mappers
{
    public static class UserWithCocktailMapper
    {
        public static BLLAppDTO.UserWithCocktail MapToBLL(PublicApiDTOv1.UserWithCocktailCreate userWithCocktailCreate) 
        {
            return new()
            {
                UserId = userWithCocktailCreate.UserId,
                CocktailId = userWithCocktailCreate.CocktailId,
                GradeId = userWithCocktailCreate.GradeId,
                Comment = userWithCocktailCreate.Comment
            };
        }
        
        public static BLLAppDTO.UserWithCocktail MapToBLL(PublicApiDTOv1.UserWithCocktailUpdate userWithCocktailUpdate) 
        {
            return new()
            {
                Id = userWithCocktailUpdate.Id,
                UserId = userWithCocktailUpdate.UserId,
                CocktailId = userWithCocktailUpdate.CocktailId,
                GradeId = userWithCocktailUpdate.GradeId,
                Comment = userWithCocktailUpdate.Comment
            };
        }

        public static PublicApiDTOv1.UserWithCocktail MapToPublic(BLLAppDTO.UserWithCocktail userWithCocktail)
        {
            return new()
            {
                Id = userWithCocktail.Id,
                UserId = userWithCocktail.UserId,
                CocktailId = userWithCocktail.CocktailId,
                CocktailName = userWithCocktail.CocktailName,
                GradeId = userWithCocktail.GradeId,
                GradeValue = userWithCocktail.GradeValue,
                Comment = userWithCocktail.Comment
            };
        }
    }
}
