using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Additive : DomainEntityId
    {
        [MaxLength(64)] public string Name { get; set; } = default!;

        public ICollection<AdditiveInCocktail>? AdditivesInCocktails { get; set; }
    }
}
