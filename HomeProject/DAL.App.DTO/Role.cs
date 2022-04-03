using System;
using Contracts.Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace DAL.App.DTO
{
    public class Role : IdentityRole<Guid>, IDomainEntityId
    {
    }
}