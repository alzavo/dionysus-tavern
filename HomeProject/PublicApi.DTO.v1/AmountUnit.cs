using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class AmountUnit
    {
        public Guid Id { get; set; }
        
        [MaxLength(32)] public string Name { get; set; } = default!;

        public int UsageCount { get; set; }
    }

    public class AmountUnitUpdate
    {
        public Guid Id { get; set; }
        
        [MaxLength(32)] public string Name { get; set; } = default!;
    }
    
    public class AmountUnitCreate
    {
        [MaxLength(32)] public string Name { get; set; } = default!;
    }
}
