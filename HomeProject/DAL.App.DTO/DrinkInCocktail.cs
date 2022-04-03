using System;
using Domain.Base;

namespace DAL.App.DTO
{
    public class DrinkInCocktail : DomainEntityId
    {
        public int Amount { get; set; }

        public Guid DrinkId { get; set; }
        public string DrinkName { get; set; } = default!;

        public Guid AmountUnitId { get; set; }
        public string AmountUnitName { get; set; } = default!;

        public Guid CocktailId { get; set; }
        public string CocktailName { get; set; } = default!;
    }
}
