using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Drink : DomainEntityId
    {
        public Guid DrinkTypeId { get; set; }
        public DrinkType DrinkType { get; set; } = default!;
        
        [MaxLength(64)] public string Name { get; set; } = default!;
        
        public int Abv { get; set; }
        
        public ICollection<DrinkInCocktail>? DrinksInCocktails { get; set; }
    }
}