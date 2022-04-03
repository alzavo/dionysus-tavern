using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class AmountUnit : DomainEntityId
    {
        [MaxLength(32)] public string Name { get; set; } = default!;

        public ICollection<DrinkInCocktail>? DrinkInCocktails { get; set; }

        public ICollection<AdditiveInCocktail>? AdditivesInCocktails { get; set; }
    }
}