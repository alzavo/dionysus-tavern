using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Drink : DomainEntityId
    {
        public Guid DrinkTypeId { get; set; }
        
        [MaxLength(64)] public string DrinkType { get; set; } = default!;
        
        [MaxLength(64)] public string Name { get; set; } = default!;
        
        public int Abv { get; set; }

        public int CocktailsCount { get; set; }
    }
}