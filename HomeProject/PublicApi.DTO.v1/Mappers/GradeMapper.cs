using BLLAppDTO = BLL.App.DTO;
using PublicApiDTOv1 = PublicApi.DTO.v1;

namespace PublicApi.DTO.v1.Mappers
{
    public class GradeMapper
    {
        public static BLLAppDTO.Grade MapToBLL(PublicApiDTOv1.GradeCreate gradeCreate) 
        {
            return new()
            {
                GradeValue = gradeCreate.GradeValue
            };
        }
        
        public static BLLAppDTO.Grade MapToBLL(PublicApiDTOv1.GradeUpdate gradeUpdate) 
        {
            return new()
            {
                Id = gradeUpdate.Id,
                GradeValue = gradeUpdate.GradeValue
            };
        }

        public static PublicApiDTOv1.Grade MapToPublic(BLLAppDTO.Grade grade)
        {
            return new()
            {
                Id = grade.Id,
                GradeValue = grade.GradeValue,
                UsageCount = grade.UsageCount
            };
        }
    }
}
