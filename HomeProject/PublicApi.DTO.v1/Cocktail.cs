using System;
using System.Collections.Generic;

namespace PublicApi.DTO.v1
{
    public class Cocktail
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; } = default!;
        
        public bool Alcoholic { get; set; }
        
        public int IngredientsCount { get; set; }
        
        public int StepsCount { get; set; }

        public int UsersCount { get; set; }
    }
    
    public class CocktailUpdate
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; } = default!;
        
        public bool Alcoholic { get; set; }
    }
    
    public class CocktailCreate
    {
        public string Name { get; set; } = default!;
        
        public bool Alcoholic { get; set; }
    }
    
    public class CocktailDetailed
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; } = default!;
        
        public bool Alcoholic { get; set; }
        
        public int UsersCount { get; set; }
        
        public ICollection<DrinkInCocktail> DrinksInCocktail { get; set; } = default!;
        
        public ICollection<AdditiveInCocktail> AdditivesInCocktail { get; set; } = default!;
        
        public ICollection<Step> Steps { get; set; } = default!;
    }
}
