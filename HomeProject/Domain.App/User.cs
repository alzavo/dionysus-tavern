using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;
using Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.App
{
    public class User : IdentityUser<Guid>, IDomainEntityId
    {
        public ICollection<UserWithCocktail>? UsersWithCocktails { get; set; }
    }
}