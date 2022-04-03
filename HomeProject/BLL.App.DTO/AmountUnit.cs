using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class AmountUnit : DomainEntityId
    {
        [MaxLength(32)] public string Name { get; set; } = default!;

        public int UsageCount { get; set; }
    }
}
