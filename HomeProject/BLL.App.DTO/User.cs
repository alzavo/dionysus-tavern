using System;
using Contracts.Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace BLL.App.DTO
{
    public class User : IdentityUser<Guid>, IDomainEntityId
    {
        public int CocktailsCount { get; set; }
    }
}
