using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;
using Domain.Base;

namespace Domain.App
{
    public class Step : DomainEntityId
    {
        public Guid CocktailId { get; set; }
        public Cocktail Cocktail { get; set; } = default!;

        [MaxLength(255)] public string Description { get; set; } = default!;

        public int IndexNumber { get; set; }
    }
}