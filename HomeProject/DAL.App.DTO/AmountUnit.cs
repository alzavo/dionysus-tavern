using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class AmountUnit : DomainEntityId
    {
        [MaxLength(32)] public string Name { get; set; } = default!;

        public int UsageCount { get; set; }
    }
} 
