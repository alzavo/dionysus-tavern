using BLLAppDTO = BLL.App.DTO;
using PublicApiDTOv1 = PublicApi.DTO.v1;

namespace PublicApi.DTO.v1.Mappers
{
    public static class UserMapper
    {
        public static PublicApiDTOv1.User MapToPublic(BLLAppDTO.User user)
        {
            return new()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                CocktailsCount = user.CocktailsCount
            };
        }
    }
}
