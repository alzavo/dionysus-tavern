using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class DrinkTypeCreate
    { 
        [MaxLength(30)] public string Type { get; set; } = default!;
    }
    
    public class DrinkTypeUpdate
    {
        public Guid Id { get; set; }
        [MaxLength(30)] public string Type { get; set; } = default!;
    }
    
    public class DrinkType
    {
        public Guid Id { get; set; }
        
        [MaxLength(30)] public string Type { get; set; } = default!;
        
        public int DrinksCount { get; set; }
    }
}
