using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Step : DomainEntityId
    {
        public Guid CocktailId { get; set; }
        
        [MaxLength(255)] public string Description { get; set; } = default!;
        
        public int IndexNumber { get; set; }
        
        public string CocktailName { get; set; } = default!;
    }
}
