using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class DrinkType : DomainEntityId
    {
        [MaxLength(64)] public string Type { get; set; } = default!;

        public int DrinksCount { get; set; }
    }
}
