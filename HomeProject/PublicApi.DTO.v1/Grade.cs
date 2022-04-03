using System;

namespace PublicApi.DTO.v1
{
    public class Grade
    {
        public Guid Id { get; set; }
        
        public string GradeValue { get; set; } = default!;
        
        public int UsageCount { get; set; }
    }
    
    public class GradeUpdate
    {
        public Guid Id { get; set; }
        
        public string GradeValue { get; set; } = default!;
    }
    
    public class GradeCreate
    {
        public string GradeValue { get; set; } = default!;
    }
}
