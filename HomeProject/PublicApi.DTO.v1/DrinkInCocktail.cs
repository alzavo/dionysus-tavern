using System;

namespace PublicApi.DTO.v1
{
    public class DrinkInCocktail
    {
        public Guid Id { get; set; }

        public Guid CocktailId { get; set; }
        public string CocktailName { get; set; } = default!;

        public Guid DrinkId { get; set; }
        public string DrinkName { get; set; } = default!;

        public Guid AmountUnitId { get; set; }
        public string AmountUnitName { get; set; } = default!;

        public int Amount { get; set; }
    }
    
    public class DrinkInCocktailUpdate
    {
        public Guid Id { get; set; }

        public Guid CocktailId { get; set; }

        public Guid DrinkId { get; set; }

        public Guid AmountUnitId { get; set; }

        public int Amount { get; set; }
    }
    
    public class DrinkInCocktailCreate
    {
        public Guid CocktailId { get; set; }

        public Guid DrinkId { get; set; }

        public Guid AmountUnitId { get; set; }

        public int Amount { get; set; }
    }
}