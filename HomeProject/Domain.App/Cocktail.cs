using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{ 
    public class Cocktail : DomainEntityId
    {
        [MaxLength(64)] public string Name { get; set; } = default!;

        public bool Alcoholic { get; set; }
        

        public ICollection<UserWithCocktail>? UsersWithCocktails { get; set; }

        public ICollection<DrinkInCocktail>? DrinksInCocktails { get; set; }

        public ICollection<AdditiveInCocktail>? AdditivesInCocktails { get; set; }

        public ICollection<Step>? Steps { get; set; }
    }
}