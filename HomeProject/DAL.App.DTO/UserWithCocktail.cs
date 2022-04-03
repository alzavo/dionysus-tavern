using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;
using Domain.Base;

namespace DAL.App.DTO
{
    public class UserWithCocktail : DomainEntityId, IDomainUserId
    {
        public Guid UserId { get; set; }

        public Guid CocktailId { get; set; }
        
        public string CocktailName { get; set; } = default!;

        public Guid GradeId { get; set; }
        
        public string GradeValue { get; set; } = default!;

        [MaxLength(255)] public string Comment { get; set; } = default!;
    }
}
