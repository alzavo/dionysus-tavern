using System;
using Domain.Base;

namespace Domain.App
{
    public class AdditiveInCocktail : DomainEntityId
    { 
        public int Amount { get; set; }

        public Guid CocktailId { get; set; }
        public Cocktail Cocktail { get; set; } = default!;

        public Guid AmountUnitId { get; set; }
        public AmountUnit AmountUnit { get; set; } = default!;
        
        public Guid AdditiveId { get; set; }
        public Additive Additive { get; set; } = default!;
    }
}