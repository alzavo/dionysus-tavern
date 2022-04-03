using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class DrinkCreate
    {
        public Guid DrinkTypeId { get; set; }

        [MaxLength(64)] public string Name { get; set; } = default!;
        
        public int Abv { get; set; }
    }

    public class DrinkUpdate
    {
        public Guid Id { get; set; }
        
        public Guid DrinkTypeId { get; set; }
        
        [MaxLength(64)] public string Name { get; set; } = default!;
        
        public int Abv { get; set; }
    }
    
    public class Drink
    {
        public Guid Id { get; set; }
        
        public Guid DrinkTypeId { get; set; }
        
        [MaxLength(64)] public string DrinkType { get; set; } = default!;
        
        [MaxLength(64)] public string Name { get; set; } = default!;
        
        public int Abv { get; set; }
        
        public int CocktailsCount { get; set; }
    }
}