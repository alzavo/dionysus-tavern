using System;
using Domain.Base;

namespace BLL.App.DTO
{
    public class AdditiveInCocktail : DomainEntityId
    { 
        public int Amount { get; set; }

        public Guid CocktailId { get; set; }
        public string CocktailName { get; set; } = default!;

        public Guid AmountUnitId { get; set; }
        public string AmountUnitName { get; set; } = default!;
        
        public Guid AdditiveId { get; set; }
        public string AdditiveName { get; set; } = default!;
    }
}
