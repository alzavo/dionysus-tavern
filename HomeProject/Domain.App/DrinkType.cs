using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class DrinkType : DomainEntityId
    {
        [MaxLength(30)] public string Type { get; set; } = default!;

        public ICollection<Drink>? Drinks { get; set; }
    }
}
