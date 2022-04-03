using Domain.Base;

namespace BLL.App.DTO
{
    public class Grade : DomainEntityId
    {
        public string GradeValue { get; set; } = default!;
        
        public int UsageCount { get; set; }
    }
}
