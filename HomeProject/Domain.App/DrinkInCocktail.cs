using System;
using Domain.Base;

namespace Domain.App
{
    public class DrinkInCocktail : DomainEntityId
    {
        public int Amount { get; set; }

        public Guid DrinkId { get; set; }
        public Drink Drink { get; set; } = default!;

        public Guid AmountUnitId { get; set; }
        public AmountUnit AmountUnit { get; set; } = default!;

        public Guid CocktailId { get; set; }
        public Cocktail Cocktail { get; set; } = default!;
    }
}