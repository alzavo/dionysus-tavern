using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;
using Domain.Base;

namespace Domain.App
{
    public class UserWithCocktail : DomainEntityId, IDomainUserId, IDomainUser<User>
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;

        public Guid CocktailId { get; set; }
        public Cocktail Cocktail { get; set; } = default!;

        public Guid GradeId { get; set; }
        public Grade Grade { get; set; } = default!;

        [MaxLength(255)] public string Comment { get; set; } = default!;
    }
}