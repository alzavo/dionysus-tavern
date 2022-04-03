using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Additive : DomainEntityId
    {
        [MaxLength(64)] public string Name { get; set; } = default!;
        
        public int CocktailsCount { get; set; }
    }
}
