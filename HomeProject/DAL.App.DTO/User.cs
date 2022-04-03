using System;
using Contracts.Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace DAL.App.DTO
{
    public class User : IdentityUser<Guid>, IDomainEntityId
    {
        public int CocktailsCount { get; set; }
    }
}
